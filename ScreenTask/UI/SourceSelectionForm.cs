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
        private readonly Func<List<ICaptureSource>>? _refreshProvider;

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

        public SourceSelectionForm(
            IReadOnlyList<ICaptureSource> sources,
            ICaptureSource? currentSource = null,
            bool captureMouse = true,
            Func<List<ICaptureSource>>? refreshProvider = null)
        {
            _refreshProvider = refreshProvider;
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
                foreach (var card in _displayCards.Concat(_windowCards))
                {
                    card.UpdateThumbnail(null);
                    card.Dispose();
                }
            };
        }

        private void InitializeCustomComponents()
        {
            Text = "Select Video Source – ScreenTask 2.0";
            Size = new Size(720, 520);
            MinimumSize = new Size(520, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MaximizeBox = true;
            MinimizeBox = false;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                Padding = new Padding(12, 8, 12, 8)
            };

            var lblTitle = new Label
            {
                Text = "Select a Screen or Application Window to Share",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(12, 13),
                AutoSize = true
            };

            btnRefresh = new Button
            {
                Text = "Refresh List",
                Size = new Size(95, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 260, 10),
                UseVisualStyleBackColor = true
            };
            btnRefresh.Click += (s, e) =>
            {
                if (_refreshProvider != null)
                {
                    var refreshed = _refreshProvider();
                    PopulateSources(refreshed, SelectedSource);
                }
                else
                {
                    PopulateSources(_allSources.ToList(), SelectedSource);
                }
            };

            cbCaptureMouse = new CheckBox
            {
                Text = "Show Mouse Cursor",
                Size = new Size(145, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 155, 12),
                Checked = true
            };

            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(cbCaptureMouse);
            Controls.Add(panelTop);

            var panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                Padding = new Padding(12, 8, 12, 8)
            };

            lblSelectedSource = new Label
            {
                Text = "Selected: None",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(12, 15),
                AutoSize = true
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Size = new Size(88, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 100, 9),
                UseVisualStyleBackColor = true
            };

            btnSelect = new Button
            {
                Text = "Share Selected",
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 228, 9),
                UseVisualStyleBackColor = true
            };

            btnSelect.Click += (s, e) =>
            {
                if (_selectedCard?.Source != null)
                {
                    SourceSelected?.Invoke(_selectedCard.Source);
                }
                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancel.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            panelBottom.Controls.Add(lblSelectedSource);
            panelBottom.Controls.Add(btnSelect);
            panelBottom.Controls.Add(btnCancel);
            Controls.Add(panelBottom);

            tabControlSources = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F),
                Padding = new Point(12, 4)
            };

            tabDisplays = new TabPage
            {
                Text = "Monitors / Displays",
                Padding = new Padding(6),
                UseVisualStyleBackColor = true
            };

            flowDisplays = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(4)
            };
            tabDisplays.Controls.Add(flowDisplays);

            tabWindows = new TabPage
            {
                Text = "Application Windows",
                Padding = new Padding(6),
                UseVisualStyleBackColor = true
            };

            flowWindows = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(4)
            };
            tabWindows.Controls.Add(flowWindows);

            tabControlSources.TabPages.Add(tabDisplays);
            tabControlSources.TabPages.Add(tabWindows);
            Controls.Add(tabControlSources);
            tabControlSources.BringToFront();

            AcceptButton = btnSelect;
            CancelButton = btnCancel;
        }

        public void PopulateSources(IReadOnlyList<ICaptureSource> sources, ICaptureSource? selectedSource = null)
        {
            _allSources.Clear();
            _allSources.AddRange(sources);

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
                    if (_selectedCard?.Source != null)
                    {
                        SourceSelected?.Invoke(_selectedCard.Source);
                    }
                    DialogResult = DialogResult.OK;
                    Close();
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
                    card.IsSelected = true;
                    _selectedCard = card;
                }
            }

            if (_selectedCard == null)
            {
                var firstCard = _displayCards.FirstOrDefault() ?? _windowCards.FirstOrDefault();
                if (firstCard != null)
                {
                    firstCard.IsSelected = true;
                    _selectedCard = firstCard;
                }
            }

            if (_selectedCard?.Source is WindowCaptureSource)
            {
                tabControlSources.SelectedTab = tabWindows;
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

            lblSelectedSource.Text = _selectedCard?.Source != null
                ? $"Selected: {_selectedCard.Source.DisplayName}"
                : "Selected: None";
        }

        private void TriggerThumbnailUpdate()
        {
            if (_isUpdatingThumbnails || IsDisposed) return;
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
                        if (IsDisposed || card.IsDisposed) break;
                        var src = card.Source;
                        if (src == null || !src.IsValid) continue;

                        var thumb = CaptureThumbnail(src, 192, 108);
                        if (thumb != null)
                        {
                            try
                            {
                                if (IsHandleCreated && !IsDisposed)
                                {
                                    BeginInvoke(() =>
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
                catch { }
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

                var thumb = new Bitmap(maxWidth, maxHeight);
                using (var g = Graphics.FromImage(thumb))
                {
                    g.InterpolationMode = InterpolationMode.Bilinear;
                    g.DrawImage(frame, 0, 0, maxWidth, maxHeight);
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
