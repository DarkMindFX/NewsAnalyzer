

using System.Text.Json.Serialization;

namespace DMFX.NewsAnalysis.DTO
{
    public class Article : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Title")]
		public System.String Title { get; set; }

				[JsonPropertyName("Content")]
		public System.String Content { get; set; }

				[JsonPropertyName("Timestamp")]
		public System.DateTime Timestamp { get; set; }

				[JsonPropertyName("NewsSourceID")]
		public System.Int64 NewsSourceID { get; set; }

				
    }
}
