namespace SooperView
{
    /// <summary>
    /// Holds all encoding configuration chosen by the user.
    /// No UI references — plain data only.
    /// </summary>
    public class EncoderSettings
    {
        public int Hardware { get; set; }   // 0=CPU 1=Nvidia 2=Intel 3=AMD
        public int Encoding { get; set; }   // 0=H264 1=H265 2=AV1
        public int Colorspace { get; set; } // 0=8-bit 1=10-bit
        public int Tune { get; set; }       // 0=none … 6=zerolatency
        public int Resolution { get; set; } // 0=4K 1=1440p 2=1080p 3=720p
        public int CRF { get; set; }
        public string Preset { get; set; } = "";
    }
}
