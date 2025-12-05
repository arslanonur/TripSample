using System.Text.Json.Serialization;

namespace TripSample.Domain.DTO
{
    public class BrowserModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
