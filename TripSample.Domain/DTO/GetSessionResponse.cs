using System.Text.Json.Serialization;
using TripSample.Domain.Model;

namespace TripSample.Domain.DTO
{
    public class GetSessionResponse
    {
        [JsonPropertyName("data")]
        public SessionModel Data { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]

        public string Message { get; set; }
    }
    
}
