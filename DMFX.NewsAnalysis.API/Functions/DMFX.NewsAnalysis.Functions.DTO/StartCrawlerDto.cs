using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Functions.DTO
{
    public class StartCrawlerDto
    {
        [JsonPropertyName("header")]
        public MessageHeader Header { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("skip_existing")]
        public bool SkipExisting { get; set; }
    }
}
