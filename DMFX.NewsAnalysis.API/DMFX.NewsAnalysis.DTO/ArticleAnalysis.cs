

using System.Text.Json.Serialization;

namespace DMFX.NewsAnalysis.DTO
{
    public class ArticleAnalysis : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Timestamp")]
		public System.DateTime Timestamp { get; set; }

				[JsonPropertyName("ArticleID")]
		public System.Int64 ArticleID { get; set; }

				[JsonPropertyName("SentimentID")]
		public System.Int64 SentimentID { get; set; }

				[JsonPropertyName("AnalyzerID")]
		public System.Int64 AnalyzerID { get; set; }

				
    }
}
