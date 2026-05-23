using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SooperView
{
    public partial class Form1 : Form
    {
        private string _xmapFilePath = Path.Combine(Path.GetTempPath(), "xmap.pgm");
        private string _ymapFilePath = Path.Combine(Path.GetTempPath(), "ymap.pgm");
        private double _videoDuration;
        private Process _process;
        private string _currentSourceFileName;
        private string _currentDestinationFileName;
        private int _currentFileIndex = -1;
        private int _totalFiles = 0;
        private bool _processing = false;

        private Dictionary<int, string[]> presets = new Dictionary<int, string[]>();
        private Dictionary<string, int> presetDefaults = new Dictionary<string, int>();
        public Form1()
        {
            InitializeComponent();
            PopulatePresets();
            PopulatePresetDefaults();
            cmbHardware.SelectedIndex = 0; //cpu
            cmbEncoding.SelectedIndex = 1; //h265
            cmbColorspace.SelectedIndex = 1; //10-bit
            cmbTune.SelectedIndex = 0; //none
            cmbResolution.SelectedIndex = 0; //4k
            SelectDefaultPreset();
            lblVersion.Text = $"v{Application.ProductVersion}";

        }

        private void PopulatePresets()
        {
            //cpu (lib264, lib265)
            presets.Add(0, new string[] { "ultrafast", "superfast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow", "placebo" });
            //nvidia
            presets.Add(1, new string[] { "p1", "p2", "p3", "p4", "p5", "p6", "p7" });
            //intel
            presets.Add(2, new string[] { "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow" });
            //amd
            presets.Add(3, new string[] { "quality", "balance", "speed" });
            //cpu (av1)
            presets.Add(4, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });

        }

        private void PopulatePresetDefaults()
        {
            //cpu libx264
            presetDefaults.Add("00", 5);
            //cpu libx265
            presetDefaults.Add("01", 5);
            //cpu av1
            presetDefaults.Add("02", 6);
            //nvidia x264
            presetDefaults.Add("10", 3);
            //nvidia x265
            presetDefaults.Add("11", 3);
            //nvidia av1
            presetDefaults.Add("12", 3);
            //intel x264
            presetDefaults.Add("20", 3);
            //intel x265
            presetDefaults.Add("21", 3);
            //intel av1
            presetDefaults.Add("22", 3);
            //amd x264
            presetDefaults.Add("30", 1);
            //amd x265
            presetDefaults.Add("31", 1);
            //amd av1
            presetDefaults.Add("32", 1);
        }

        private void btnSooperViewIt_Click(object sender, EventArgs e)
        {
            ClearLog();
            _totalFiles = lvFiles.Items.Count;
            _currentFileIndex = -1;
            Task.Run(() =>
            {
                _processing = true;
                while (_processing)
                {
                    StartNextSooperItProcess();

                    if (_processing)
                    {
                        if (_currentFileIndex + 1 < _totalFiles)
                        {
                            StartNextSooperItProcess();
                        }
                        else
                        {
                            _processing = false;
                        }
                    }
                }

                ResetUI();
            });
        }

        private void StartNextSooperItProcess()
        {
            _currentFileIndex++;
            lvFiles.Invoke(() =>
            {
                ListViewItem item = lvFiles.Items[_currentFileIndex];
                _currentSourceFileName = item.SubItems[0].Text;
                _currentDestinationFileName = item.SubItems[1].Text;
            });

            if (File.Exists(_currentSourceFileName))
            {
                var vidProperties = GetVideoProperties(_currentSourceFileName);
                if (vidProperties != null)
                {
                    if ((vidProperties.Height * 4 / 3) == vidProperties.Width)
                    {
                        CreateRemapFiles(vidProperties);
                        if (File.Exists(_xmapFilePath) && File.Exists(_ymapFilePath))
                        {
                            this._videoDuration = vidProperties.Duration.HasValue ? vidProperties.Duration.Value : 0;
                            SooperItProcess();
                        }
                        else
                        {
                            UpdateLog("There was a problem generating the remap files!");
                        }
                    }
                    else
                    {
                        UpdateLog($"{_currentSourceFileName} is not a 4:3 video file!");
                    }
                }
                else
                {
                    UpdateLog($"{_currentSourceFileName} is not a 4:3 video file!");
                }
            }
            else
            {
                UpdateLog($"{_currentSourceFileName} doesn't exist!");
            }
        }

        private void ClearLog()
        {
            lvLog.Invoke(() =>
            {
                lvLog.Items.Clear();
            });
        }
        private void UpdateLog(string log)
        {
            lvLog.Invoke(() =>
            {
                lvLog.Items.Add(log);
            });
        }

        /* 
         * Superview Algorithm
         * https://intofpv.com/t-using-free-command-line-sorcery-to-fake-superview
        */
        private double DerpIt(double tx, int targetWidth, int srcWidth)
        {
            double x = (tx / targetWidth - 0.5) * 2; // -1 -> 1
            double sx = tx - (targetWidth - srcWidth) / 2.0;
            double offset = Math.Pow(x, 2) * (x < 0 ? -1 : 1) * ((targetWidth - srcWidth) / 2.0);
            return sx - offset;
        }

        private VideoProperties? GetVideoProperties(string filePath)
        {
            //ffprobe -v error -show_entries stream=width,height,duration -of json DJI_0669.MP4
            //filePath = @"C:\Users\Stacey Abshire\Videos\djio3\2025\01\11\DJI_0669.mp4";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg\\ffprobe",
                    Arguments = $"-i \"{filePath}\" -show_entries stream=width,height,duration -of json",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            VideoInfo? vidInfo = JsonConvert.DeserializeObject<VideoInfo>(result);
            VideoProperties? properties = null;
            if (vidInfo != null)
            {
                if (vidInfo.StreamProperties != null)
                {
                    if (vidInfo.StreamProperties.Count > 0)
                    {
                        properties = vidInfo.StreamProperties[0];
                    }
                }
            }
            process.WaitForExit();
            process.Close();
            process = null;
            return properties;

        }
        private void CreateRemapFiles(VideoProperties vidProperties)
        {
            int sourceWidth = vidProperties.Width;
            int sourceHeight = vidProperties.Height;
            int targetWidth = vidProperties.Height * 16 / 9;

            using (StreamWriter xmap = new StreamWriter(_xmapFilePath))
            {
                xmap.WriteLine($"P2 {targetWidth} {sourceHeight} 65535");

                for (int y = 0; y < sourceHeight; y++)
                {
                    for (int x = 0; x < targetWidth; x++)
                    {
                        double fudgeit = DerpIt(x, targetWidth, sourceWidth);
                        xmap.Write($"{(int)fudgeit} ");
                    }
                    xmap.WriteLine();
                }
            }

            using (StreamWriter ymap = new StreamWriter(_ymapFilePath))
            {
                ymap.WriteLine($"P2 {targetWidth} {sourceHeight} 65535");

                for (int y = 0; y < sourceHeight; y++)
                {
                    for (int x = 0; x < targetWidth; x++)
                    {
                        ymap.Write($"{y} ");
                    }
                    ymap.WriteLine();
                }
            }
        }

        private void SooperItProcess()
        {
            btnSooperViewIt.Invoke(() =>
            {
                btnSooperViewIt.Enabled = false;
            });
            btnCancel.Invoke(() =>
            {
                btnCancel.Enabled = true;
            });
            lvFiles.Invoke(() =>
            {
                lvFiles.Enabled = false;
            });
            cmbColorspace.Invoke(() =>
            {
                cmbColorspace.Enabled = false;
            });
            cmbHardware.Invoke(() =>
            {
                cmbHardware.Enabled = false;
            });
            cmbEncoding.Invoke(() =>
            {
                cmbEncoding.Enabled = false;
            });
            nudCRF.Invoke(() =>
            {
                nudCRF.Enabled = false;
            });

            SooperItProcessThread();
        }

        private void SooperItProcessThread()
        {
            bool completed = false;
            btnCancel.BeginInvoke(() =>
            {
                btnCancel.Text = $"Cancel (Progress: {_currentFileIndex + 1} of {_totalFiles} / 0.0%)";
            });


            string encoder = "libx265";
            int hardware = 0;
            int encoding = 0;
            string crf = "";
            int colorspace = 0;
            int tune = 0;
            string preset = "";
            int resolution = 0;

            cmbHardware.Invoke(() =>
            {
                hardware = cmbHardware.SelectedIndex;
            });
            cmbEncoding.Invoke(() =>
            {
                encoding = cmbEncoding.SelectedIndex;
            });
            nudCRF.Invoke(() =>
            {
                switch (cmbHardware.SelectedIndex)
                {
                    case 1:
                        //nvidia
                        crf = $"-rc constqp -cq:v {(int)nudCRF.Value} -b:v 0";
                        break;
                    case 2:
                        //intel
                        crf = $"-global_quality {(int)nudCRF.Value}";
                        break;
                    case 3:
                        //amd
                        crf = $"-rc cqp -qp_i {(int)nudCRF.Value} -qp_p {(int)nudCRF.Value}";
                        break;
                    default:
                        crf = $"-crf {(int)nudCRF.Value}";
                        break;

                }
            });
            cmbColorspace.Invoke(() =>
            {
                colorspace = cmbColorspace.SelectedIndex;
            });
            cmbTune.Invoke(() =>
            {
                tune = cmbTune.SelectedIndex;
            });

            cmbPreset.Invoke(() =>
            {
                preset = (string)cmbPreset.Text;
            });
            cmbResolution.Invoke(() =>
            {
                resolution = cmbResolution.SelectedIndex;
            });

            switch (hardware)
            {
                case 0: //CPU
                    switch (encoding)
                    {
                        case 0: //h624
                            encoder = "libx264";
                            break;
                        case 1: //h265
                            encoder = "libx265";
                            break;
                        case 2: //av1
                            encoder = "libsvtav1";
                            break;
                    }
                    break;
                case 1: //Nvidia
                    switch (encoding)
                    {
                        case 0: //h624
                            encoder = "h264_nvenc";
                            break;
                        case 1: //h265
                            encoder = "hevc_nvenc";
                            break;
                        case 2: //av1
                            encoder = "av1_nvenc";
                            break;
                    }
                    break;
                case 2: //Intel
                    switch (encoding)
                    {
                        case 0: //h624
                            encoder = "h264_qsv";
                            break;
                        case 1: //h265
                            encoder = "hevc_qsv";
                            break;
                        case 2: //av1
                            encoder = "av1_qsv";
                            break;
                    }
                    break;
                case 3: //AMD
                    switch (encoding)
                    {
                        case 0: //h624
                            encoder = "h264_amf";
                            break;
                        case 1: //h265
                            encoder = "hevc_amf";
                            break;
                        case 2: //av1
                            encoder = "av1_amf";
                            break;
                    }
                    break;
            }

            string pixfmt = "yuv420p10le";
            switch (colorspace)
            {
                case 0: // 8-bit
                    pixfmt = "yuv420p";
                    break;
                case 1: //10-bit
                    pixfmt = "yuv420p10le";
                    break;
            }
            string tuneString = "";
            switch (tune)
            {
                case 0:
                    tuneString = "";
                    break;
                case 1:
                    tuneString = "-tune film";
                    break;
                case 2:
                    tuneString = "-tune grain";
                    break;
                case 3:
                    tuneString = "-tune animation";
                    break;
                case 4:
                    tuneString = "-tune stillimage";
                    break;
                case 5:
                    tuneString = "-tune fastdecode";
                    break;
                case 6:
                    tuneString = "-tune zerolatency";
                    break;

            }

            string scale = "";
            switch (resolution)
            {
                case 0:
                    scale = "scale=3840:2160";
                    break;
                case 1:
                    scale = "scale=2560:1440";
                    break;
                case 2:
                    scale = "scale=1920:1080";
                    break;
                case 3:
                    scale = "scale=1280:720";
                    break;
            }


            string processArgs = $"-i \"{_currentSourceFileName}\" -i \"{_xmapFilePath}\" -i \"{_ymapFilePath}\" -filter_complex \"[0:v][1:v][2:v]remap,{scale}\" -c:v {encoder} {crf} -pix_fmt {pixfmt} {tuneString} -preset {preset} -y \"{_currentDestinationFileName}\"";

            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg\\ffmpeg",
                    Arguments = processArgs,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            _process.Start();
            //string line = outLine.Data;
            var timeRegex = new Regex(@"time=(\d+):(\d+):(\d+).(\d+)", RegexOptions.Compiled);
            string line;
            try
            {
                while ((line = _process.StandardError.ReadLine()) != null)
                {
                    // Parse the "time=" field to get the current progress
                    var match = timeRegex.Match(line);
                    if (match.Success)
                    {
                        // Convert time (hours, minutes, seconds, milliseconds) to seconds
                        double hours = double.Parse(match.Groups[1].Value);
                        double minutes = double.Parse(match.Groups[2].Value);
                        double seconds = double.Parse(match.Groups[3].Value);
                        double milliseconds = double.Parse(match.Groups[4].Value);

                        double currentProgress = hours * 3600 + minutes * 60 + seconds + milliseconds / 100;
                        float percentage = (float)(currentProgress / _videoDuration * 100);
                        prgProcess.BeginInvoke(() =>
                        {
                            prgProcess.Value = (int)percentage;
                            prgProcess.Update();
                        });
                        btnCancel.BeginInvoke(() =>
                        {
                            btnCancel.Text = $"Cancel (Progress: {_currentFileIndex + 1} of {_totalFiles} / {percentage:F2}%)";
                        });
                    }
                    else
                    {
                        UpdateLog(line);
                    }
                }
            }
            catch (Exception e)
            {
                UpdateLog(e.Message);
            }

            //handle if killed by cancel and not normal ending
            if (_process != null)
            {
                completed = true;
                _process.WaitForExit();
                _process.Close();
                _process = null;
            }

            if (!completed)
            {
                _processing = false;
            }

            if (!_processing)
            {
                ResetUI();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CancelProcessing();
        }

        private void ResetUI()
        {
            lvFiles.BeginInvoke(() =>
            {
                lvFiles.Enabled = true;
            });

            btnSooperViewIt.BeginInvoke(() =>
            {
                btnSooperViewIt.Enabled = true;
            });
            prgProcess.BeginInvoke(() =>
            {
                prgProcess.Value = 0;
            });
            btnCancel.BeginInvoke(() =>
            {
                btnCancel.Enabled = false;
                btnCancel.Text = "Cancel";
            });

            cmbColorspace.BeginInvoke(() =>
            {
                cmbColorspace.Enabled = true;
            });

            cmbHardware.BeginInvoke(() =>
            {
                cmbHardware.Enabled = true;
            });

            cmbEncoding.BeginInvoke(() =>
            {
                cmbEncoding.Enabled = true;
            });

            nudCRF.BeginInvoke(() =>
            {
                nudCRF.Enabled = true;
            });

        }

        private void CancelProcessing()
        {
            if (_process != null)
            {
                _process.Kill();
                _process.Close();
                _process = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelProcessing();
            btnCancel.Enabled = false;
            btnCancel.Text = "Cancel";
        }

        private void lvFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lvFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if ((!fileAdded(file)))
                {
                    string newFile = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + "_SV" + Path.GetExtension(file));
                    string[] f = { file, newFile };
                    ListViewItem lvItem = new ListViewItem(f);
                    lvFiles.Items.Add(lvItem);
                }
            }
        }

        private bool fileAdded(string file)
        {
            foreach (ListViewItem itm in lvFiles.Items)
            {
                if (itm.SubItems[0].Text.CompareTo(file) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void lvFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                var selected = lvFiles.SelectedItems;
                for (int i = selected.Count - 1; i >= 0; i--)
                {
                    lvFiles.Items.RemoveAt(lvFiles.Items.IndexOf(selected[i]));
                }
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                // Efficiently update the UI
                lvFiles.BeginUpdate();
                try
                {
                    foreach (ListViewItem item in lvFiles.Items)
                    {
                        item.Selected = true;
                    }
                }
                finally
                {
                    lvFiles.EndUpdate();
                }
            }
        }

        private void cmbHardware_SelectedIndexChanged(object sender, EventArgs e)
        {
            int hardware = 0;
            if (cmbHardware.SelectedIndex == 0)
            {
                if (cmbEncoding.SelectedIndex == 2) //av1
                {
                    hardware = 4;
                }
                else
                {
                    hardware = 0;
                }
            }
            else
            {
                hardware |= cmbHardware.SelectedIndex;
            }
            UpdatePresets(hardware);
        }

        private void cmbEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEncoding.SelectedIndex == 2) //av1
            {
                if (cmbHardware.SelectedIndex == 0) //cpu
                {
                    UpdatePresets(4);
                }
            }
            else
            {
                UpdatePresets(cmbHardware.SelectedIndex);
            }
        }

        private void UpdatePresets(int hardware)
        {
            cmbPreset.Items.Clear();
            foreach (var presetStrings in presets[hardware])
            {
                cmbPreset.Items.Add(presetStrings);
            }
            SelectDefaultPreset();
        }
        private void SelectDefaultPreset()
        {
            if (cmbEncoding.SelectedIndex < 0) { return; }
            if (cmbHardware.SelectedIndex < 0) { return; }
            string hardwareEncod = $"{cmbHardware.SelectedIndex}{cmbEncoding.SelectedIndex}";
            cmbPreset.SelectedIndex = presetDefaults[hardwareEncod];
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // Fill the header background with your choice of color
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), e.Bounds);

            // Draw the header text (using default or custom font/brush)
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, e.Bounds, Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
