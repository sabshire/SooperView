using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SooperView
{
    /// <summary>
    /// Payload carried by the ProgressChanged event.
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        public int    CurrentFile  { get; init; }
        public int    TotalFiles   { get; init; }
        public float  Percentage   { get; init; }
    }

    /// <summary>
    /// Orchestrates ffprobe + remap-file generation + ffmpeg for a list of files.
    /// Communicates back to the UI exclusively through events — no Control references.
    /// </summary>
    public class VideoProcessor
    {
        // ── Events ────────────────────────────────────────────────────────────
        public event EventHandler<ProgressEventArgs>? ProgressChanged;
        public event EventHandler<string>?            LogMessage;
        public event EventHandler?                    ProcessingStarted;
        public event EventHandler?                    ProcessingFinished;

        // ── Paths ─────────────────────────────────────────────────────────────
        private readonly string _xmapFilePath = Path.Combine(Path.GetTempPath(), "xmap.pgm");
        private readonly string _ymapFilePath = Path.Combine(Path.GetTempPath(), "ymap.pgm");

        // ── State ─────────────────────────────────────────────────────────────
        private Process?  _process;
        private bool      _processing;
        private double    _videoDuration;

        private readonly RemapFileGenerator _remapGenerator = new();

        // ── Public API ────────────────────────────────────────────────────────

        /// <summary>
        /// Starts encoding all files in <paramref name="filePairs"/> on a background thread.
        /// Each entry is (sourcePath, destinationPath).
        /// </summary>
        public void StartAsync(IReadOnlyList<(string Source, string Destination)> filePairs,
                               EncoderSettings settings)
        {
            Task.Run(() => RunAll(filePairs, settings));
        }

        /// <summary>
        /// Cancels the currently running ffmpeg process (if any).
        /// </summary>
        public void Cancel()
        {
            if (_process != null)
            {
                _processing = false;
                _process.Kill();
                _process.Close();
                _process = null;
            }
        }

        // ── Internal pipeline ─────────────────────────────────────────────────

        private void RunAll(IReadOnlyList<(string Source, string Destination)> filePairs,
                            EncoderSettings settings)
        {
            _processing = true;
            ProcessingStarted?.Invoke(this, EventArgs.Empty);

            for (int i = 0; i < filePairs.Count && _processing; i++)
            {
                ProcessFile(i, filePairs.Count, filePairs[i].Source, filePairs[i].Destination, settings);
            }

            ProcessingFinished?.Invoke(this, EventArgs.Empty);
        }

        private void ProcessFile(int index, int total, string source, string destination,
                                 EncoderSettings settings)
        {
            if (!File.Exists(source))
            {
                Log($"{source} doesn't exist!");
                return;
            }

            var vidProperties = GetVideoProperties(source);
            if (vidProperties == null)
            {
                Log($"{source} is not a valid video file!");
                return;
            }

            if ((vidProperties.Height * 4 / 3) != vidProperties.Width)
            {
                Log($"{source} is not a 4:3 video file!");
                return;
            }

            _remapGenerator.CreateRemapFiles(vidProperties, _xmapFilePath, _ymapFilePath);

            if (!File.Exists(_xmapFilePath) || !File.Exists(_ymapFilePath))
            {
                Log("There was a problem generating the remap files!");
                return;
            }

            _videoDuration = vidProperties.Duration ?? 0;
            RunFfmpeg(index, total, source, destination, settings);
        }

        private VideoProperties? GetVideoProperties(string filePath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName              = "ffmpeg\\ffprobe",
                    Arguments             = FfmpegArgumentBuilder.BuildFfprobeArguments(filePath),
                    RedirectStandardOutput = true,
                    UseShellExecute       = false,
                    CreateNoWindow        = true
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();

            var vidInfo = JsonConvert.DeserializeObject<VideoInfo>(result);
            return vidInfo?.StreamProperties?.Count > 0
                ? vidInfo.StreamProperties[0]
                : null;
        }

        private void RunFfmpeg(int index, int total, string source, string destination,
                               EncoderSettings settings)
        {
            string args = FfmpegArgumentBuilder.BuildFfmpegArguments(
                source, destination, _xmapFilePath, _ymapFilePath, settings);

            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName             = "ffmpeg\\ffmpeg",
                    Arguments            = args,
                    RedirectStandardError = true,
                    UseShellExecute      = false,
                    CreateNoWindow       = true
                }
            };

            _process.Start();

            ReportProgress(index, total, 0f);

            var timeRegex = new Regex(@"time=(\d+):(\d+):(\d+)\.(\d+)", RegexOptions.Compiled);
            try
            {
                string? line;
                while ((line = _process.StandardError.ReadLine()) != null)
                {
                    var match = timeRegex.Match(line);
                    if (match.Success)
                    {
                        double hours        = double.Parse(match.Groups[1].Value);
                        double minutes      = double.Parse(match.Groups[2].Value);
                        double seconds      = double.Parse(match.Groups[3].Value);
                        double milliseconds = double.Parse(match.Groups[4].Value);

                        double current    = hours * 3600 + minutes * 60 + seconds + milliseconds / 100;
                        float  percentage = _videoDuration > 0
                            ? (float)(current / _videoDuration * 100)
                            : 0f;

                        ReportProgress(index, total, percentage);
                    }
                    else
                    {
                        Log(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }

            if (_process != null)
            {
                _process.WaitForExit();
                _process.Close();
                _process = null;
            }
        }

        public bool CheckIfFFmpegExists()
        {
            // This doesn't check if system wide ffmpeg exists, but we don't currently use system-wide ffmpeg
            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg"))) {
                bool returnValue = true;
                if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg", "ffmpeg.exe"))) { LogHelper.LogMessageColor(this, "VideoProcessor: Missing ffmpeg.exe inside ffmpeg folder!", Color.Red); returnValue = false; } // Checks if ffmpeg.exe is missing
                if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg", "ffprobe.exe"))) { LogHelper.LogMessageColor(this, "VideoProcessor: Missing ffprobe.exe inside ffmpeg folder!", Color.Red); returnValue = false; } // Checks if ffprobe.exe is missing
                return returnValue; // Return true if all files exist
            } else { LogHelper.LogMessageColor(this, "VideoProcessor: Missing ffmpeg folder!", Color.Red); return false; } // ffmpeg folder doesn't exist
        }

        private void ReportProgress(int index, int total, float percentage) =>
            ProgressChanged?.Invoke(this, new ProgressEventArgs
            {
                CurrentFile = index + 1,
                TotalFiles  = total,
                Percentage  = percentage
            });

        private void Log(string message) =>
            LogMessage?.Invoke(this, message);
    }
}
