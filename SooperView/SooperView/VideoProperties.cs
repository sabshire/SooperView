using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SooperView
{
    internal class VideoProperties
    {
        private double? _duration;

        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("duration")]
        public double? Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                this._duration = value.HasValue ? value.Value : null;
            }
        }
    }
}
