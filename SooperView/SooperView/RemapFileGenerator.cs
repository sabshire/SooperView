namespace SooperView
{
    /// <summary>
    /// Generates the xmap/ymap PGM remap files used by ffmpeg's remap filter.
    /// Implements the SuperView stretch algorithm.
    /// No UI references.
    /// </summary>
    public class RemapFileGenerator
    {
        /*
         * SuperView Algorithm
         * https://intofpv.com/t-using-free-command-line-sorcery-to-fake-superview
         */
        private static double DerpIt(double tx, int targetWidth, int srcWidth)
        {
            double x = (tx / targetWidth - 0.5) * 2; // -1 -> 1
            double sx = tx - (targetWidth - srcWidth) / 2.0;
            double offset = Math.Pow(x, 2) * (x < 0 ? -1 : 1) * ((targetWidth - srcWidth) / 2.0);
            return sx - offset;
        }

        /// <summary>
        /// Writes xmap and ymap PGM files to the given paths.
        /// </summary>
        public void CreateRemapFiles(VideoProperties vidProperties, string xmapFilePath, string ymapFilePath)
        {
            int sourceWidth = vidProperties.Width;
            int sourceHeight = vidProperties.Height;
            int targetWidth = vidProperties.Height * 16 / 9;

            using (StreamWriter xmap = new StreamWriter(xmapFilePath))
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

            using (StreamWriter ymap = new StreamWriter(ymapFilePath))
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
    }
}
