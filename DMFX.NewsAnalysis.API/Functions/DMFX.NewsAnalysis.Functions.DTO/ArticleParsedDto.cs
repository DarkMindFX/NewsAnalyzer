using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DMFX.NewsAnalysis.Functions.DTO
{
    public class ArticleParsedDto
    {
        [JsonPropertyName("header")]
        public MessageHeader Header { get; set; }

        [JsonPropertyName("article_id")]
        public long ArticleID { get; set; }
    }
}
