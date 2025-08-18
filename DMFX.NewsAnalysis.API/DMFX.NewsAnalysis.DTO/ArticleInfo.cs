using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.DTO
{
    public class ArticleInfo
    {
        [JsonPropertyName("ID")]
        public System.Int64 ID { get; set; }

        [JsonPropertyName("Title")]
        public System.String Title { get; set; }

        [JsonPropertyName("PublishedDateTime")]
        public System.DateTime PublishedDateTime { get; set; }
    }
}
