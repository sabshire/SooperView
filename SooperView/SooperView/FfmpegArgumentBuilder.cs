namespace SooperView
{
    /// <summary>
    /// Translates EncoderSettings into concrete ffmpeg/ffprobe argument strings.
    /// No UI references, no I/O — pure string logic.
    /// </summary>
    public static class FfmpegArgumentBuilder
    {
        private static readonly Dictionary<(int hw, int enc), string> EncoderMap = new()
        {
            { (0, 0), "libx264"    }, { (0, 1), "libx265"    }, { (0, 2), "libsvtav1"  },
            { (1, 0), "h264_nvenc" }, { (1, 1), "hevc_nvenc" }, { (1, 2), "av1_nvenc"  },
            { (2, 0), "h264_qsv"   }, { (2, 1), "hevc_qsv"   }, { (2, 2), "av1_qsv"    },
            { (3, 0), "h264_amf"   }, { (3, 1), "hevc_amf"   }, { (3, 2), "av1_amf"    },
        };

        private static readonly string[] TuneFlags =
        {
            "",                     // 0 – none
            "-tune film",           // 1
            "-tune grain",          // 2
            "-tune animation",      // 3
            "-tune stillimage",     // 4
            "-tune fastdecode",     // 5
            "-tune zerolatency",    // 6
        };

        private static readonly string[] ScaleFilters =
        {
            "scale=3840:2160",  // 0 – 4K
            "scale=2560:1440",  // 1 – 1440p
            "scale=1920:1080",  // 2 – 1080p
            "scale=1280:720",   // 3 – 720p
        };

        public static string GetEncoder(EncoderSettings s) =>
            EncoderMap.TryGetValue((s.Hardware, s.Encoding), out var enc) ? enc : "libx265";

        public static string GetCrfArgument(EncoderSettings s) => s.Hardware switch
        {
            1 => $"-rc constqp -cq:v {s.CRF} -b:v 0",     // Nvidia
            2 => $"-global_quality {s.CRF}",                // Intel
            3 => $"-rc cqp -qp_i {s.CRF} -qp_p {s.CRF}",  // AMD
            _ => $"-crf {s.CRF}",                           // CPU
        };

        public static string GetPixelFormat(EncoderSettings s) =>
            s.Colorspace == 0 ? "yuv420p" : "yuv420p10le";

        public static string GetTuneArgument(EncoderSettings s) =>
            s.Tune >= 0 && s.Tune < TuneFlags.Length ? TuneFlags[s.Tune] : "";

        public static string GetScaleFilter(EncoderSettings s) =>
            s.Resolution >= 0 && s.Resolution < ScaleFilters.Length ? ScaleFilters[s.Resolution] : ScaleFilters[0];

        public static string BuildFfprobeArguments(string filePath) =>
            $"-i \"{filePath}\" -show_entries stream=width,height,duration -of json";

        public static string BuildFfmpegArguments(
            string sourceFile, string destFile,
            string xmapPath, string ymapPath,
            EncoderSettings settings)
        {
            string encoder    = GetEncoder(settings);
            string crf        = GetCrfArgument(settings);
            string pixFmt     = GetPixelFormat(settings);
            string tune       = GetTuneArgument(settings);
            string scale      = GetScaleFilter(settings);
            string tuneArg    = string.IsNullOrEmpty(tune) ? "" : $"{tune} ";

            return $"-i \"{sourceFile}\" -i \"{xmapPath}\" -i \"{ymapPath}\" " +
                   $"-filter_complex \"[0:v][1:v][2:v]remap,{scale}\" " +
                   $"-c:v {encoder} {crf} -pix_fmt {pixFmt} {tuneArg}" +
                   $"-preset {settings.Preset} -y \"{destFile}\"";
        }
    }
}
