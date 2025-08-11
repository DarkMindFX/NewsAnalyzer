

using System.Text.Json.Serialization;

namespace DMFX.NewsAnalysis.DTO
{
    public class NewsSource : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				[JsonPropertyName("Url")]
		public System.String Url { get; set; }

				[JsonPropertyName("IsActive")]
		public System.Boolean IsActive { get; set; }

				
    }
}
