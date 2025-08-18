using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.API.Helpers
{
    public class ServiceConfig
    {
        [JsonPropertyName("DALType")]
        public string DALType { get; set; }

        [JsonPropertyName("DALInitParams")]
        public Dictionary<string, string> DALInitParams { get; set; }
    }
}
