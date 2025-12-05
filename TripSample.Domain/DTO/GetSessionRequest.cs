using System.Text.Json.Serialization;

namespace TripSample.Domain.DTO
{
    public class GetSessionRequest 
    {
        [JsonPropertyName("type")]
        public required int Type { get; set; }
        [JsonPropertyName("connection")]
        public required ConnectionModel Connection { get; set; }
        [JsonPropertyName("browser")]
        public required BrowserModel Browser { get; set; }
    }

    

    
}
