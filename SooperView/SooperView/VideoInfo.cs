using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SooperView
{
    internal class VideoInfo
    {
        [JsonProperty("programs")]
        public List<object>? Programs;
        [JsonProperty("stream_groups")]
        public List<object>? StreamGroups;
        [JsonProperty("streams")]
        public List<VideoProperties>? StreamProperties;
    }
}
