using System.Reflection;

namespace SooperView
{
    public partial class Form1 : Form
    {
        // ── Process logic ──────────────────────────────────────────────────────
        private readonly VideoProcessor _processor = new();

        // ── Preset tables (UI-only configuration data) ─────────────────────────
        private readonly Dictionary<int, string[]> _presets = new();
        private readonly Dictionary<string, int>   _presetDefaults = new();

        public Form1()
        {
            InitializeComponent();

            PopulatePresets();
            PopulatePresetDefaults();
            SelectHardwareDefault();

            cmbEncoding.SelectedIndex   = 1; // h265
            cmbColorspace.SelectedIndex = 1; // 10-bit
            cmbTune.SelectedIndex       = 0; // none
            cmbResolution.SelectedIndex = 0; // 4K
            SelectDefaultPreset();

            lblVersion.Text = $"v{Application.ProductVersion}";
            SetupTooltips();

            // Wire processor events to UI updates
            _processor.ProgressChanged    += OnProgressChanged;
            _processor.LogMessage         += (_, msg) => UpdateLog(msg);
            _processor.ProcessingStarted  += (_, _)   => SetProcessingState(true);
            _processor.ProcessingFinished += (_, _)   => SetProcessingState(false);
        }

        // ── Hardware auto-select ───────────────────────────────────────────────

        private void SelectHardwareDefault()
        {
            var detector = new HardwareDetector();
            cmbHardware.SelectedIndex =
                detector.DetectNVIDIAGPUs()  ? 1 :
                detector.DetectAMDGPUs()     ? 2 :
                detector.DetectIntelGPUs()   ? 3 : 0;
        }

        // ── Button handlers ────────────────────────────────────────────────────

        private void btnSooperViewIt_Click(object sender, EventArgs e)
        {
            ClearLog();

            var files = lvFiles.Items
                .Cast<ListViewItem>()
                .Select(i => (Source: i.SubItems[0].Text, Destination: i.SubItems[1].Text))
                .ToList();

            if (files.Count == 0) return;

            _processor.StartAsync(files, ReadSettings());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _processor.Cancel();
            btnCancel.Enabled = false;
            btnCancel.Text    = "Cancel";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) =>
            _processor.Cancel();

        // ── Settings snapshot ──────────────────────────────────────────────────

        private EncoderSettings ReadSettings() => new()
        {
            Hardware   = cmbHardware.SelectedIndex,
            Encoding   = cmbEncoding.SelectedIndex,
            Colorspace = cmbColorspace.SelectedIndex,
            Tune       = cmbTune.SelectedIndex,
            Resolution = cmbResolution.SelectedIndex,
            CRF        = (int)nudCRF.Value,
            Preset     = cmbPreset.Text,
        };

        // ── Processor event handlers ───────────────────────────────────────────

        private void OnProgressChanged(object? sender, ProgressEventArgs e)
        {
            prgProcess.BeginInvoke(() =>
            {
                prgProcess.Value = (int)e.Percentage;
                prgProcess.Update();
            });
            btnCancel.BeginInvoke(() =>
            {
                btnCancel.Text = $"Cancel (Progress: {e.CurrentFile} of {e.TotalFiles} / {e.Percentage:F2}%)";
            });
        }

        // ── UI state helpers ───────────────────────────────────────────────────

        private void SetProcessingState(bool processing)
        {
            bool enable = !processing;
            btnSooperViewIt.BeginInvoke(() => btnSooperViewIt.Enabled = enable);
            btnCancel.BeginInvoke(() =>
            {
                btnCancel.Enabled = processing;
                if (!processing) btnCancel.Text = "Cancel";
            });

            lvFiles.BeginInvoke(()       => lvFiles.Enabled      = enable);
            cmbColorspace.BeginInvoke(() => cmbColorspace.Enabled = enable);
            cmbHardware.BeginInvoke(()   => cmbHardware.Enabled  = enable);
            cmbTune.BeginInvoke(() => cmbTune.Enabled = enable && cmbEncoding.SelectedIndex == 0 && cmbHardware.SelectedIndex == 0);
            cmbResolution.BeginInvoke(() => cmbResolution.Enabled = enable);
            cmbPreset.BeginInvoke(() => cmbPreset.Enabled = enable);
            cmbEncoding.BeginInvoke(() => cmbEncoding.Enabled = enable);
            nudCRF.BeginInvoke(() => nudCRF.Enabled = enable);

            if (!processing)
                prgProcess.BeginInvoke(() => prgProcess.Value = 0);
        }

        private void ClearLog() =>
            lvLog.Invoke(() => lvLog.Items.Clear());

        private void UpdateLog(string message) =>
            lvLog.Invoke(() => lvLog.Items.Add(message));

        // ── File list interactions ─────────────────────────────────────────────

        private void lvFiles_DragEnter(object sender, DragEventArgs e) =>
            e.Effect = e.Data?.GetDataPresent(DataFormats.FileDrop) == true
                ? DragDropEffects.Copy
                : DragDropEffects.None;

        private void lvFiles_DragDrop(object sender, DragEventArgs e)
        {
            lblFileDrop.Visible = false;
            string[] files = (string[])e.Data!.GetData(DataFormats.FileDrop)!;
            foreach (string file in files)
            {
                if (!FileAlreadyAdded(file))
                {
                    string dest = Path.Combine(
                        Path.GetDirectoryName(file)!,
                        Path.GetFileNameWithoutExtension(file) + "_SV" + Path.GetExtension(file));

                    lvFiles.Items.Add(new ListViewItem(new[] { file, dest }));
                }
            }
        }

        private bool FileAlreadyAdded(string file) =>
            lvFiles.Items.Cast<ListViewItem>()
                   .Any(i => i.SubItems[0].Text == file);

        private void lvFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                var selected = lvFiles.SelectedItems;
                for (int i = selected.Count - 1; i >= 0; i--)
                    lvFiles.Items.RemoveAt(lvFiles.Items.IndexOf(selected[i]));

                lblFileDrop.Visible = lvFiles.Items.Count == 0;
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                lvFiles.BeginUpdate();
                try   { foreach (ListViewItem item in lvFiles.Items) item.Selected = true; }
                finally { lvFiles.EndUpdate(); }
            }
        }

        // ── Preset management ──────────────────────────────────────────────────

        private void PopulatePresets()
        {
            _presets[0] = new[] { "ultrafast","superfast","veryfast","faster","fast","medium","slow","slower","veryslow","placebo" };
            _presets[1] = new[] { "p1","p2","p3","p4","p5","p6","p7" };
            _presets[2] = new[] { "veryfast","faster","fast","medium","slow","slower","veryslow" };
            _presets[3] = new[] { "quality","balance","speed" };
            _presets[4] = new[] { "0","1","2","3","4","5","6","7","8","9","10","11","12" };
        }

        private void PopulatePresetDefaults()
        {
            _presetDefaults["00"] = 5; _presetDefaults["01"] = 5; _presetDefaults["02"] = 6;
            _presetDefaults["10"] = 3; _presetDefaults["11"] = 3; _presetDefaults["12"] = 3;
            _presetDefaults["20"] = 3; _presetDefaults["21"] = 3; _presetDefaults["22"] = 3;
            _presetDefaults["30"] = 1; _presetDefaults["31"] = 1; _presetDefaults["32"] = 1;
        }

        private void UpdatePresets(int hardwareKey)
        {
            cmbPreset.Items.Clear();
            foreach (var p in _presets[hardwareKey])
                cmbPreset.Items.Add(p);
            SelectDefaultPreset();
        }

        private void SelectDefaultPreset()
        {
            if (cmbEncoding.SelectedIndex < 0 || cmbHardware.SelectedIndex < 0) return;
            string key = $"{cmbHardware.SelectedIndex}{cmbEncoding.SelectedIndex}";
            if (_presetDefaults.TryGetValue(key, out int idx))
                cmbPreset.SelectedIndex = idx;
        }

        // ── ComboBox change handlers ───────────────────────────────────────────

        private void cmbHardware_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isTuneAvailable = cmbEncoding.SelectedIndex == 0 && cmbHardware.SelectedIndex == 0;
            cmbTune.Enabled = isTuneAvailable;
            if (!isTuneAvailable) cmbTune.SelectedIndex = 0;

            int key = cmbHardware.SelectedIndex == 0 && cmbEncoding.SelectedIndex == 2
                ? 4
                : cmbHardware.SelectedIndex;
            UpdatePresets(key);
        }

        private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isTuneAvailable = cmbEncoding.SelectedIndex == 0 && cmbHardware.SelectedIndex == 0;
            cmbTune.Enabled = isTuneAvailable;
            if (!isTuneAvailable) cmbTune.SelectedIndex = 0;

            int key = cmbEncoding.SelectedIndex == 2 && cmbHardware.SelectedIndex == 0
                ? 4
                : cmbHardware.SelectedIndex;
            UpdatePresets(key);
        }

        // ── Custom list-view drawing ───────────────────────────────────────────

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), e.Bounds);
            TextRenderer.DrawText(e.Graphics, e.Header?.Text, e.Font, e.Bounds,
                Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e) =>
            e.DrawDefault = true;

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e) =>
            e.DrawDefault = true;

        // ── Tooltips ──────────────────────────────────────────────────────────

        private void SetupTooltips()
        {
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(lblFileDrop,   "Drop files here to process.\n\nYou can select files by clicking on them, and remove them from the queue with the DELETE or BACKSPACE key.\n\n");
            toolTip.SetToolTip(lvFiles,       "Drop files here to process.\n\nYou can select files by clicking on them, and remove them from the queue with the DELETE or BACKSPACE key.\n\n");
            toolTip.SetToolTip(nudCRF,        "Valid values from 0 to 51.\n\n0 is losless encoding, while 51 is the worst possible encoding.\nValue of 17 or 18 is visually losless or very close.");
            toolTip.SetToolTip(cmbColorspace, "10bit color or 8bit color");
            toolTip.SetToolTip(cmbEncoding,   "The type of encoding to use for the output video.");
            toolTip.SetToolTip(cmbHardware,   "CPU or GPU (Nvidia, Intel, or AMD) encoding.\n\nCPU encoding is slower, but produces marginally better quality.\nGPU encoding is much faster.  Choose your brand of GPU");
            toolTip.SetToolTip(cmbPreset,     "Encoding presets, higher numerical value is better.  Slower encoding is better.");
            toolTip.SetToolTip(cmbResolution, "The output resolution for the encoded video.");
            toolTip.SetToolTip(cmbTune,       "Tune x264 video based on the type of video.\n\nNone - Don't tune the video.\nGrain - preserves the grain structure in old, grainy film material\nFilm - use for high quality movie content; lowers deblocking\nAnimation - good for cartoons; uses higher deblocking and more reference frames\nStill Image - good for slideshow-like content\nFast Decode - allows faster decoding by disabling certain filters\nZero Latency - good for fast encoding and low-latency streaming");
        }
    }
}
