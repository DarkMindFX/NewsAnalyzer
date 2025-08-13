using System.Text.Json.Serialization;

namespace DMFX.NewsAnalysis.Functions.DTO
{
    public class MessageHeader
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("sender")]
        public string Sender { get; set; }
    }
}
