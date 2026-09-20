using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenTask.Core.Capture;
using ScreenTask.UI.Controls;

namespace ScreenTask.UI.Views
{
    public partial class CaptureSettingsView : UserControl
    {
        public enum SourceFilterTab
        {
            Displays,
            Windows
        }

        private readonly List<SourceCardControl> _displayCards = new();
        private readonly List<SourceCardControl> _windowCards = new();
        private readonly List<ICaptureSource> _allSources = new();
        private SourceFilterTab _activeTab = SourceFilterTab.Displays;
        private SourceCardControl? _selectedCard;
        private readonly System.Windows.Forms.Timer _thumbnailTimer = new() { Interval = 2000 };
        private bool _isUpdatingThumbnails = false;

        public event EventHandler? RefreshScreensClicked;
        public event EventHandler? ScreenSelectionChanged;
        public event EventHandler? CaptureMouseChanged;
        public event EventHandler? AudioToggled;
        public event EventHandler? AudioDeviceChanged;
        public event EventHandler? RefreshAudioClicked;
        public event EventHandler? TestAudioClicked;
        public event EventHandler? AudioDiagClicked;
        public event EventHandler? FpsChanged;
        public event EventHandler? QualityChanged;
        public event Action<ICaptureSource>? SourceCardSelected;

        public SourceFilterTab ActiveTab => _activeTab;
        public IReadOnlyList<SourceCardControl> DisplayCards => _displayCards;
        public IReadOnlyList<SourceCardControl> WindowCards => _windowCards;
        public SourceCardControl? SelectedCard => _selectedCard;
        public Label SelectedSourceLabel => lblSelectedSource;
        public string SelectedSourceText => lblSelectedSource.Text;

        public CaptureSettingsView()
        {
            InitializeComponent();

            btnRefreshScreens.Click += (s, e) => RefreshScreensClicked?.Invoke(this, EventArgs.Empty);
            comboScreens.SelectedIndexChanged += OnComboScreensSelectedIndexChanged;
            cbCaptureMouse.CheckedChanged += (s, e) => CaptureMouseChanged?.Invoke(this, EventArgs.Empty);

            btnTabDisplays.Click += (s, e) => SwitchTab(SourceFilterTab.Displays);
            btnTabWindows.Click += (s, e) => SwitchTab(SourceFilterTab.Windows);

            cbAudio.CheckedChanged += (s, e) =>
            {
                bool audioOn = cbAudio.Checked;
                comboAudioDevices.Enabled = audioOn;
                btnRefreshAudio.Enabled = audioOn;
                btnTestAudio.Enabled = audioOn;
                btnAudioDiag.Enabled = audioOn;
                AudioToggled?.Invoke(this, EventArgs.Empty);
            };

            comboAudioDevices.SelectedIndexChanged += (s, e) => AudioDeviceChanged?.Invoke(this, EventArgs.Empty);
            btnRefreshAudio.Click += (s, e) => RefreshAudioClicked?.Invoke(this, EventArgs.Empty);
            btnTestAudio.Click += (s, e) => TestAudioClicked?.Invoke(this, EventArgs.Empty);
            btnAudioDiag.Click += (s, e) => AudioDiagClicked?.Invoke(this, EventArgs.Empty);

            comboFps.SelectedIndexChanged += (s, e) => FpsChanged?.Invoke(this, EventArgs.Empty);
            qualitySlider.ValueChanged += (s, e) =>
            {
                lblQuality.Text = $"Image Quality: {qualitySlider.Value}% ({(qualitySlider.Value >= 80 ? "High Detail" : qualitySlider.Value >= 50 ? "Balanced" : "Low Bandwidth")})";
                QualityChanged?.Invoke(this, EventArgs.Empty);
            };

            flowSourceCards.Resize += (s, e) => UpdateHeaderWidths();

            this.Resize += (s, e) => UpdateLayout();
            _thumbnailTimer.Tick += OnThumbnailTimerTick;
            this.VisibleChanged += (s, e) =>
            {
                if (this.Visible)
                {
                    UpdateLayout();
                    _thumbnailTimer.Start();
                    TriggerThumbnailUpdate();
                }
                else
                {
                    _thumbnailTimer.Stop();
                }
            };
        }

        private void UpdateLayout()
        {
            if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0) return;
            int totalWidth = this.ClientSize.Width;
            int totalHeight = this.ClientSize.Height;

            int bottomH = 160;
            int bottomY = Math.Max(260, totalHeight - bottomH);

            // Card Video Source (Grid of Display/App Source Cards)
            cardVideoSource.Location = new Point(0, 0);
            cardVideoSource.Size = new Size(totalWidth, bottomY - 8);

            flowSourceCards.Location = new Point(10, 38);
            flowSourceCards.Size = new Size(Math.Max(300, cardVideoSource.ClientSize.Width - 20), Math.Max(180, cardVideoSource.ClientSize.Height - 74));

            lblSelectedSource.Location = new Point(12, cardVideoSource.ClientSize.Height - 28);
            lblSelectedSource.Width = Math.Max(200, cardVideoSource.ClientSize.Width - 24);

            // Audio Card and Quality/Framerate Card
            if (totalWidth >= 640)
            {
                int halfW = (totalWidth - 8) / 2;
                cardAudio.Location = new Point(0, bottomY);
                cardAudio.Size = new Size(halfW, bottomH);

                groupBox1.Location = new Point(halfW + 8, bottomY);
                groupBox1.Size = new Size(totalWidth - (halfW + 8), bottomH);
            }
            else
            {
                cardAudio.Location = new Point(0, bottomY);
                cardAudio.Size = new Size(totalWidth, bottomH);

                groupBox1.Location = new Point(0, bottomY + bottomH + 8);
                groupBox1.Size = new Size(totalWidth, bottomH);
            }

            // Internal Audio controls
            lblAudioPill.Location = new Point(Math.Max(160, cardAudio.ClientSize.Width - lblAudioPill.Width - 12), 10);
            btnRefreshAudio.Location = new Point(cardAudio.ClientSize.Width - btnRefreshAudio.Width - 12, 69);
            comboAudioDevices.Location = new Point(16, 70);
            comboAudioDevices.Width = Math.Max(120, btnRefreshAudio.Left - comboAudioDevices.Left - 8);

            // Internal Quality controls
            qualitySlider.Location = new Point(16, 126);
            qualitySlider.Width = Math.Max(150, groupBox1.ClientSize.Width - 32);
            comboFps.Width = Math.Min(240, groupBox1.ClientSize.Width - 32);
        }

        public void SwitchTab(SourceFilterTab tab)
        {
            _activeTab = tab;
            UpdateTabButtonsState();
            if (tab == SourceFilterTab.Windows && _windowCards.Count > 0)
            {
                flowSourceCards.ScrollControlIntoView(_windowCards[0]);
            }
            else if (tab == SourceFilterTab.Displays && _displayCards.Count > 0)
            {
                flowSourceCards.ScrollControlIntoView(_displayCards[0]);
            }
        }

        private void UpdateTabButtonsState()
        {
            if (_activeTab == SourceFilterTab.Displays)
            {
                btnTabDisplays.Font = new Font(btnTabDisplays.Font, FontStyle.Bold);
                btnTabDisplays.BackColor = SystemColors.Control;
                btnTabWindows.Font = new Font(btnTabWindows.Font, FontStyle.Regular);
                btnTabWindows.BackColor = SystemColors.Control;
            }
            else
            {
                btnTabDisplays.Font = new Font(btnTabDisplays.Font, FontStyle.Regular);
                btnTabDisplays.BackColor = SystemColors.Control;
                btnTabWindows.Font = new Font(btnTabWindows.Font, FontStyle.Bold);
                btnTabWindows.BackColor = SystemColors.Control;
            }
        }

        public void PopulateSources(IReadOnlyList<ICaptureSource> sources, ICaptureSource? selectedSource = null)
        {
            _allSources.Clear();
            _allSources.AddRange(sources);

            // Clean up old cards and their thumbnails
            foreach (var card in _displayCards.Concat(_windowCards))
            {
                card.UpdateThumbnail(null);
                card.Dispose();
            }
            _displayCards.Clear();
            _windowCards.Clear();
            _selectedCard = null;

            // Also synchronize comboScreens items for backwards compatibility and tests
            comboScreens.Items.Clear();

            int selectedIndex = -1;
            int currentIndex = 0;

            foreach (var src in sources)
            {
                var card = new SourceCardControl(src);
                card.CardClicked += (s, e) => SelectCard(card, notifyEngine: true);

                if (src is ScreenCaptureSource)
                {
                    _displayCards.Add(card);
                    comboScreens.Items.Add($"🖥️ {src.DisplayName}");
                }
                else
                {
                    _windowCards.Add(card);
                    comboScreens.Items.Add($"🪟 App: {src.DisplayName}");
                }

                if (selectedSource != null && (src.Id == selectedSource.Id || src.DisplayName == selectedSource.DisplayName))
                {
                    _selectedCard = card;
                    card.IsSelected = true;
                    selectedIndex = currentIndex;
                }

                currentIndex++;
            }

            // Update tab button counts (retained for backward-compatible properties)
            btnTabDisplays.Text = $"🖥️ Entire Displays ({_displayCards.Count})";
            btnTabWindows.Text = $"🪟 App Windows ({_windowCards.Count})";

            if (_selectedCard == null)
            {
                if (_displayCards.Count > 0)
                {
                    _selectedCard = _displayCards[0];
                    _selectedCard.IsSelected = true;
                    selectedIndex = 0;
                }
                else if (_windowCards.Count > 0)
                {
                    _selectedCard = _windowCards[0];
                    _selectedCard.IsSelected = true;
                    selectedIndex = 0;
                }
            }
            else if (_windowCards.Contains(_selectedCard))
            {
                _activeTab = SourceFilterTab.Windows;
            }

            if (selectedIndex >= 0 && selectedIndex < comboScreens.Items.Count)
            {
                comboScreens.SelectedIndex = selectedIndex;
            }

            UpdateSelectedSourceLabel();
            UpdateTabButtonsState();
            RenderActiveTabCards();
            TriggerThumbnailUpdate();
        }

        private Label CreateSectionHeader(string text, int topMargin = 4)
        {
            int targetWidth = flowSourceCards.ClientSize.Width > 24
                ? Math.Max(560, flowSourceCards.ClientSize.Width - 24)
                : 600;

            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = SystemColors.ControlDarkDark,
                AutoSize = false,
                Width = targetWidth,
                Height = 24,
                Margin = new Padding(4, topMargin, 4, 4),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Label CreatePlaceholderLabel(string text)
        {
            int targetWidth = flowSourceCards.ClientSize.Width > 24
                ? Math.Max(560, flowSourceCards.ClientSize.Width - 24)
                : 600;

            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = SystemColors.GrayText,
                AutoSize = false,
                Width = targetWidth,
                Height = 28,
                Margin = new Padding(12, 2, 4, 4),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void UpdateHeaderWidths()
        {
            int targetWidth = flowSourceCards.ClientSize.Width > 24
                ? Math.Max(560, flowSourceCards.ClientSize.Width - 24)
                : 600;

            foreach (Control c in flowSourceCards.Controls)
            {
                if (c is Label && c != lblNoSources && !(c is SourceCardControl))
                {
                    c.Width = targetWidth;
                }
            }
        }

        private void RenderActiveTabCards()
        {
            flowSourceCards.SuspendLayout();
            flowSourceCards.Controls.Clear();

            if (_displayCards.Count == 0 && _windowCards.Count == 0)
            {
                lblNoSources.Text = "No capture sources found.\nClick Refresh to re-scan.";
                lblNoSources.Visible = true;
                flowSourceCards.Controls.Add(lblNoSources);
                flowSourceCards.ResumeLayout(true);
                return;
            }

            lblNoSources.Visible = false;

            // 1. Entire Displays Section
            var headerDisplays = CreateSectionHeader($"🖥️ Physical Displays ({_displayCards.Count})", topMargin: 4);
            flowSourceCards.Controls.Add(headerDisplays);
            flowSourceCards.SetFlowBreak(headerDisplays, true);

            if (_displayCards.Count > 0)
            {
                foreach (var card in _displayCards)
                {
                    flowSourceCards.Controls.Add(card);
                }
                flowSourceCards.SetFlowBreak(_displayCards.Last(), true);
            }
            else
            {
                var emptyDisplays = CreatePlaceholderLabel("No physical displays detected.");
                flowSourceCards.Controls.Add(emptyDisplays);
                flowSourceCards.SetFlowBreak(emptyDisplays, true);
            }

            // 2. Application Windows Section
            var headerWindows = CreateSectionHeader($"🪟 Application Windows ({_windowCards.Count})", topMargin: 12);
            flowSourceCards.Controls.Add(headerWindows);
            flowSourceCards.SetFlowBreak(headerWindows, true);

            if (_windowCards.Count > 0)
            {
                foreach (var card in _windowCards)
                {
                    flowSourceCards.Controls.Add(card);
                }
            }
            else
            {
                var emptyWindows = CreatePlaceholderLabel("No shareable application windows open. Click Refresh to scan.");
                flowSourceCards.Controls.Add(emptyWindows);
                flowSourceCards.SetFlowBreak(emptyWindows, true);
            }

            flowSourceCards.ResumeLayout(true);
        }

        private void SelectCard(SourceCardControl card, bool notifyEngine)
        {
            if (_selectedCard == card && card.IsSelected) return;

            // Unselect all other cards across both physical displays and application windows
            foreach (var c in _displayCards.Concat(_windowCards))
            {
                if (c != card)
                {
                    c.IsSelected = false;
                }
            }

            card.IsSelected = true;
            _selectedCard = card;
            UpdateSelectedSourceLabel();

            // Synchronize with comboScreens
            if (card.Source != null)
            {
                int index = _allSources.IndexOf(card.Source);
                if (index >= 0 && index < comboScreens.Items.Count && comboScreens.SelectedIndex != index)
                {
                    comboScreens.SelectedIndex = index;
                }

                if (notifyEngine)
                {
                    SourceCardSelected?.Invoke(card.Source);
                    ScreenSelectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void UpdateSelectedSourceLabel()
        {
            if (lblSelectedSource.InvokeRequired)
            {
                lblSelectedSource.BeginInvoke(UpdateSelectedSourceLabel);
                return;
            }

            if (_selectedCard?.Source != null)
            {
                var src = _selectedCard.Source;
                string icon = src is ScreenCaptureSource ? "🖥️" : "🪟";
                lblSelectedSource.Text = $"Selected: {icon} {src.DisplayName}";
            }
            else
            {
                lblSelectedSource.Text = "Selected: None";
            }
        }

        private void OnComboScreensSelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboScreens.SelectedIndex >= 0 && comboScreens.SelectedIndex < _allSources.Count)
            {
                var src = _allSources[comboScreens.SelectedIndex];
                var matchingCard = _displayCards.Concat(_windowCards).FirstOrDefault(c => c.Source == src);
                if (matchingCard != null && matchingCard != _selectedCard)
                {
                    SelectCard(matchingCard, notifyEngine: false);
                    flowSourceCards.ScrollControlIntoView(matchingCard);
                }
            }
            ScreenSelectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnThumbnailTimerTick(object? sender, EventArgs e)
        {
            if (!this.Visible || _isUpdatingThumbnails) return;
            TriggerThumbnailUpdate();
        }

        private void TriggerThumbnailUpdate()
        {
            if (_isUpdatingThumbnails || this.IsDisposed) return;
            _isUpdatingThumbnails = true;

            var cardsToUpdate = flowSourceCards.Controls.OfType<SourceCardControl>().ToList();
            if (cardsToUpdate.Count == 0)
            {
                _isUpdatingThumbnails = false;
                return;
            }

            _ = Task.Run(() =>
            {
                try
                {
                    foreach (var card in cardsToUpdate)
                    {
                        if (this.IsDisposed || !this.Visible || card.IsDisposed) break;
                        var src = card.Source;
                        if (src == null || !src.IsValid) continue;

                        var thumb = CaptureThumbnail(src, 182, 94);
                        if (thumb != null)
                        {
                            try
                            {
                                if (this.IsHandleCreated && !this.IsDisposed)
                                {
                                    this.BeginInvoke(() =>
                                    {
                                        if (!card.IsDisposed)
                                        {
                                            card.UpdateThumbnail(thumb);
                                        }
                                        else
                                        {
                                            thumb.Dispose();
                                        }
                                    });
                                }
                                else
                                {
                                    thumb.Dispose();
                                }
                            }
                            catch
                            {
                                thumb.Dispose();
                            }
                        }
                    }
                }
                catch
                {
                    // Ignore capture errors during window closing or resizing
                }
                finally
                {
                    _isUpdatingThumbnails = false;
                }
            });
        }

        private static Bitmap? CaptureThumbnail(ICaptureSource source, int maxWidth, int maxHeight)
        {
            try
            {
                using var frame = source.CaptureFrame(captureCursor: false);
                if (frame == null) return null;

                float scale = Math.Min((float)maxWidth / frame.Width, (float)maxHeight / frame.Height);
                int targetW = Math.Max(1, (int)(frame.Width * scale));
                int targetH = Math.Max(1, (int)(frame.Height * scale));

                var thumb = new Bitmap(targetW, targetH);
                using (var g = Graphics.FromImage(thumb))
                {
                    g.InterpolationMode = InterpolationMode.Low;
                    g.DrawImage(frame, 0, 0, targetW, targetH);
                }
                return thumb;
            }
            catch
            {
                return null;
            }
        }

        public void SetScreens(IEnumerable<string> items, int selectedIndex)
        {
            comboScreens.Items.Clear();
            foreach (var item in items) comboScreens.Items.Add(item);
            if (selectedIndex >= 0 && selectedIndex < comboScreens.Items.Count)
            {
                comboScreens.SelectedIndex = selectedIndex;
            }
        }

        public void SetAudioDevices(IEnumerable<string> items, int selectedIndex)
        {
            comboAudioDevices.Items.Clear();
            foreach (var item in items) comboAudioDevices.Items.Add(item);
            if (selectedIndex >= 0 && selectedIndex < comboAudioDevices.Items.Count)
            {
                comboAudioDevices.SelectedIndex = selectedIndex;
            }
        }

        public void SetAudioEnabled(bool enabled)
        {
            cbAudio.Checked = enabled;
            comboAudioDevices.Enabled = enabled;
            btnRefreshAudio.Enabled = enabled;
            btnTestAudio.Enabled = enabled;
            btnAudioDiag.Enabled = enabled;
        }

        public void SetQuality(int quality)
        {
            qualitySlider.Value = Math.Clamp(quality, qualitySlider.Minimum, qualitySlider.Maximum);
            lblQuality.Text = $"Image Quality: {qualitySlider.Value}% ({(qualitySlider.Value >= 80 ? "High Detail" : qualitySlider.Value >= 50 ? "Balanced" : "Low Bandwidth")})";
        }
    }
}
