

using System.Text.Json.Serialization;

namespace DMFX.NewsAnalysis.DTO
{
    public class SentimentType : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				
    }
}
