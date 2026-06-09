using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SooperView
{

    public enum EncoderPreset
    {
        CPU_H26X = 0,
        NVIDIA = 1,
        INTEL = 2,
        AMD = 3,
        CPU_AV1 = 4
    }

    public enum EncoderHardware
    {
        CPU = 0,
        NVIDIA = 1,
        INTEL = 2,
        AMD = 3
    }
    public enum Encoding
    {
        H264 = 0,
        H265 = 1,
        AVI = 2
    }
}
