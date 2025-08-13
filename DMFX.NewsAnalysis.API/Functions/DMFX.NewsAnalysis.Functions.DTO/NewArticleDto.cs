using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Functions.DTO
{
    public class NewArticleDto
    {
        [JsonPropertyName("header")]
        public MessageHeader Header { get; set; }

        [JsonPropertyName("url")]
        public string URL { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

    }
}
