using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScreenTask.Core.Capture;

namespace ScreenTask.UI
{
    public class SourceSelectionForm : Form
    {
        private readonly List<ICaptureSource> _allSources = new();
        private readonly List<SourceCard> _displayCards = new();
        private readonly List<SourceCard> _windowCards = new();
        private SourceCard? _selectedCard;
        private bool _isUpdatingThumbnails;
        private readonly System.Windows.Forms.Timer _thumbnailTimer = new();

        public TabControl tabControlSources = null!;
        public TabPage tabDisplays = null!;
        public TabPage tabWindows = null!;
        public FlowLayoutPanel flowDisplays = null!;
        public FlowLayoutPanel flowWindows = null!;
        public Button btnRefresh = null!;
        public CheckBox cbCaptureMouse = null!;
        public Label lblSelectedSource = null!;
        public Button btnSelect = null!;
        public Button btnCancel = null!;

        public ICaptureSource? SelectedSource => _selectedCard?.Source;
        public IReadOnlyList<SourceCard> DisplayCards => _displayCards;
        public IReadOnlyList<SourceCard> WindowCards => _windowCards;
        public SourceCard? SelectedCard => _selectedCard;

        public event Action<ICaptureSource>? SourceSelected;

        public SourceSelectionForm(IReadOnlyList<ICaptureSource> sources, ICaptureSource? currentSource = null, bool captureMouse = true)
        {
            InitializeCustomComponents();
            cbCaptureMouse.Checked = captureMouse;
            PopulateSources(sources, currentSource);

            _thumbnailTimer.Interval = 2000;
            _thumbnailTimer.Tick += (s, e) => TriggerThumbnailUpdate();
            _thumbnailTimer.Start();

            FormClosing += (s, e) =>
            {
                _thumbnailTimer.Stop();
                _thumbnailTimer.Dispose();
            };
        }

        private void InitializeCustomComponents()
        {
            Text = "ScreenTask 2.0 – Select Video Source";
            Size = new Size(740, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // ── Top Header Bar ──────────────────────────────────────────────
            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                Padding = new Padding(14, 8, 14, 8),
                BackColor = SystemColors.Control
            };

            var lblTitle = new Label
            {
                Text = "🖥️ Select Video Source to Share",
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(12, 8),
                AutoSize = true
            };

            var lblSubtitle = new Label
            {
                Text = "Choose a physical display monitor or an individual application window.",
                Font = new Font("Segoe UI", 8F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(14, 30),
                AutoSize = true
            };

            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Font = new Font("Segoe UI", 8.5F),
                Size = new Size(88, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 245, 12),
                UseVisualStyleBackColor = true
            };

            cbCaptureMouse = new CheckBox
            {
                Text = "Show mouse cursor",
                Font = new Font("Segoe UI", 8.5F),
                Size = new Size(135, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 150, 14),
                Checked = true
            };

            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(lblSubtitle);
            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(cbCaptureMouse);
            Controls.Add(panelTop);

            // ── Bottom Action Bar ───────────────────────────────────────────
            var panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(12, 8, 12, 8),
                BackColor = SystemColors.Control
            };

            lblSelectedSource = new Label
            {
                Text = "Selected: None",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(14, 16),
                AutoSize = true
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Size = new Size(85, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 95, 10),
                UseVisualStyleBackColor = true
            };

            btnSelect = new Button
            {
                Text = "Select Source",
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Size = new Size(115, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 218, 10),
                UseVisualStyleBackColor = true
            };

            btnSelect.Click += (s, e) =>
            {
                if (_selectedCard?.Source != null)
                {
                    SourceSelected?.Invoke(_selectedCard.Source);
                }
                Close();
            };

            btnCancel.Click += (s, e) => Close();

            panelBottom.Controls.Add(lblSelectedSource);
            panelBottom.Controls.Add(btnSelect);
            panelBottom.Controls.Add(btnCancel);
            Controls.Add(panelBottom);

            // ── Center Tab Control (Displays vs Windows) ────────────────────
            tabControlSources = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                Padding = new Point(12, 6)
            };

            tabDisplays = new TabPage
            {
                Text = "🖥️ Entire Displays",
                Padding = new Padding(8),
                BackColor = SystemColors.Window
            };

            flowDisplays = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = SystemColors.Window,
                Padding = new Padding(8)
            };
            tabDisplays.Controls.Add(flowDisplays);

            tabWindows = new TabPage
            {
                Text = "🪟 App Windows",
                Padding = new Padding(8),
                BackColor = SystemColors.Window
            };

            flowWindows = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = SystemColors.Window,
                Padding = new Padding(8)
            };
            tabWindows.Controls.Add(flowWindows);

            tabControlSources.TabPages.Add(tabDisplays);
            tabControlSources.TabPages.Add(tabWindows);
            Controls.Add(tabControlSources);

            // Ensure proper Z-ordering
            tabControlSources.BringToFront();
        }

        public void PopulateSources(IReadOnlyList<ICaptureSource> sources, ICaptureSource? selectedSource = null)
        {
            _allSources.Clear();
            _allSources.AddRange(sources);

            // Clean up old cards
            foreach (var card in _displayCards.Concat(_windowCards))
            {
                card.UpdateThumbnail(null);
                card.Dispose();
            }
            _displayCards.Clear();
            _windowCards.Clear();
            _selectedCard = null;

            flowDisplays.SuspendLayout();
            flowWindows.SuspendLayout();
            flowDisplays.Controls.Clear();
            flowWindows.Controls.Clear();

            foreach (var src in sources)
            {
                var card = new SourceCard(src);
                card.CardClicked += (s, e) => SelectCard(card);
                card.DoubleClick += (s, e) =>
                {
                    SelectCard(card);
                    btnSelect.PerformClick();
                };

                if (src is ScreenCaptureSource)
                {
                    _displayCards.Add(card);
                    flowDisplays.Controls.Add(card);
                }
                else
                {
                    _windowCards.Add(card);
                    flowWindows.Controls.Add(card);
                }

                if (selectedSource != null && (src.Id == selectedSource.Id || src.DisplayName == selectedSource.DisplayName))
                {
                    _selectedCard = card;
                    card.IsSelected = true;
                }
            }

            // Tab header badges with counts
            tabDisplays.Text = $"🖥️ Entire Displays ({_displayCards.Count})";
            tabWindows.Text = $"🪟 App Windows ({_windowCards.Count})";

            // Default selection if none matched
            if (_selectedCard == null)
            {
                if (_displayCards.Count > 0)
                {
                    _selectedCard = _displayCards[0];
                    _selectedCard.IsSelected = true;
                }
                else if (_windowCards.Count > 0)
                {
                    _selectedCard = _windowCards[0];
                    _selectedCard.IsSelected = true;
                }
            }

            // If selected is a window, switch to windows tab automatically
            if (_selectedCard != null && _windowCards.Contains(_selectedCard))
            {
                tabControlSources.SelectedTab = tabWindows;
            }
            else
            {
                tabControlSources.SelectedTab = tabDisplays;
            }

            // Placeholders if any category is empty
            if (_displayCards.Count == 0)
            {
                flowDisplays.Controls.Add(new Label
                {
                    Text = "No physical display monitors detected.",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = SystemColors.GrayText,
                    AutoSize = true,
                    Margin = new Padding(12)
                });
            }

            if (_windowCards.Count == 0)
            {
                flowWindows.Controls.Add(new Label
                {
                    Text = "No open shareable application windows detected.",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = SystemColors.GrayText,
                    AutoSize = true,
                    Margin = new Padding(12)
                });
            }

            flowDisplays.ResumeLayout(true);
            flowWindows.ResumeLayout(true);

            UpdateSelectedLabel();
            TriggerThumbnailUpdate();
        }

        private void SelectCard(SourceCard card)
        {
            foreach (var c in _displayCards.Concat(_windowCards))
            {
                c.IsSelected = (c == card);
            }
            _selectedCard = card;
            UpdateSelectedLabel();
        }

        private void UpdateSelectedLabel()
        {
            if (lblSelectedSource.InvokeRequired)
            {
                lblSelectedSource.BeginInvoke(UpdateSelectedLabel);
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

        private void TriggerThumbnailUpdate()
        {
            if (_isUpdatingThumbnails || this.IsDisposed) return;
            _isUpdatingThumbnails = true;

            var cardsToUpdate = _displayCards.Concat(_windowCards).ToList();
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
                        if (this.IsDisposed || card.IsDisposed) break;
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
                    // Ignore transient capture errors while closing
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
    }
}
