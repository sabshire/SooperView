using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;

//probably better (more accurate) ways to do this, but it's good enough for now
public class HardwareDetector
{
    private string[] _videoProcessors;

    public HardwareDetector()
    {
        _videoProcessors = GetVideoProcessors();
    }

    public bool DetectNVIDIAGPUs()
    {
        return _videoProcessors.Where(videoProcessor => videoProcessor.Contains("NVIDIA") || videoProcessor.Contains("Quadro") || videoProcessor.Contains("GeForce")).Count() > 0;
    }

    public  bool DetectAMDGPUs()
    {
        return _videoProcessors.Where(videoProcessor => videoProcessor.Contains("AMD") || videoProcessor.Contains("Radeon") || videoProcessor.Contains("FirePro")).Count() > 0;
    }
    public  bool DetectIntelGPUs()
    {
        return _videoProcessors.Where(videoProcessor => videoProcessor.Contains("Intel") || videoProcessor.Contains("UHD Graphics") || videoProcessor.Contains("Iris")).Count() > 0;
    }

    private static string[] GetVideoProcessors()
    {
        List<string> videoProcessors = new List<string>();
        using (var searcher = new System.Management.ManagementObjectSearcher(
            "SELECT VideoProcessor from Win32_VideoController"))
        {
            var gpus = searcher.Get();
            foreach (var gpusEntry in gpus)
            {
                var videoProcessor = gpusEntry["VideoProcessor"].ToString() ?? "N/A";
                videoProcessors.Add(videoProcessor);
            }
        }
        return videoProcessors.Where(s=>s.CompareTo("N/A")!=0).ToArray<string>();
    }
}
        