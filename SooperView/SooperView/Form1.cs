using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace SooperView
{
    public partial class Form1 : Form
    {
        private string _xmapFilePath = Path.Combine(Path.GetTempPath(), "xmap.pgm");
        private string _ymapFilePath = Path.Combine(Path.GetTempPath(), "ymap.pgm");
        private double _videoDuration;
        private Process _process;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSooperViewIt_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtSourceFileName.Text))
            {
                var vidProperties = GetVideoProperties(txtSourceFileName.Text);
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
                            MessageBox.Show("There was a problem generating the remap files!");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"{txtSourceFileName.Text} is not a 4:3 video file!");
                    }
                }
            }
            else
            {
                MessageBox.Show($"{txtSourceFileName.Text} doesn't exist!");
            }
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
            //ffmpeg -i DJI_0669.MP4 -i xmap.pgm -i ymap.pgm -filter_complex "[0:v][1:v][2:v]remap" -y DJI_0669_SV.MP4
            btnPickSaveAsFileName.Enabled = false;
            btnPickSourceFile.Enabled = false;
            btnSooperViewIt.Enabled = false;
            btnCancel.Enabled = true;
            txtSaveAsFileName.Enabled = false;
            txtSourceFileName.Enabled = false;
            Thread processThread = new Thread(SooperItProcessThread);
            processThread.Start();
        }

        private void SooperItProcessThread()
        {
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg\\ffmpeg",
                    Arguments = $"-i \"{txtSourceFileName.Text}\" -i \"{_xmapFilePath}\" -i \"{_ymapFilePath}\" -filter_complex \"[0:v][1:v][2:v]remap\" -y \"{txtSaveAsFileName.Text}\"",
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            //process.OutputDataReceived += SooperItProcessOutputHandler;
            _process.Start();

            //string line = outLine.Data;
            var timeRegex = new Regex(@"time=(\d+):(\d+):(\d+).(\d+)", RegexOptions.Compiled);
            string line;
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
                }
            }

            _process.WaitForExit();
            _process.Close();
          
            btnPickSaveAsFileName.BeginInvoke(() =>
            {
                btnPickSaveAsFileName.Enabled = true;
            });

            btnPickSourceFile.BeginInvoke(() =>
            {
                btnPickSourceFile.Enabled = true;
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
            });
            txtSaveAsFileName.BeginInvoke(() =>
            {
                txtSaveAsFileName.Enabled = true;
            });
            txtSourceFileName.BeginInvoke(() =>
            {
                txtSourceFileName.Enabled = true;
            });

            MessageBox.Show("Conversion Completed!");
        }

        private void btnPickSourceFile_Click(object sender, EventArgs e)
        {
            ofdPickSourceFileName.ShowDialog();
        }

        private void btnPickSaveAsFileName_Click(object sender, EventArgs e)
        {
            ofdPickSaveAsFileName.ShowDialog();
        }

        private void ofdPickSourceFileName_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtSourceFileName.Text = ofdPickSourceFileName.FileName;
            txtSaveAsFileName.Text = Path.Combine(Path.GetDirectoryName(txtSourceFileName.Text), Path.GetFileNameWithoutExtension(txtSourceFileName.Text) + "_SV" + Path.GetExtension(txtSourceFileName.Text));
        }

        private void ofdPickSaveAsFileName_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtSaveAsFileName.Text = ofdPickSaveAsFileName.FileName;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CancelProcessing();
        }

        private void CancelProcessing()
        {
            if (_process != null)
            {
                _process.Kill();
                _process.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelProcessing();
        }
    }
}
