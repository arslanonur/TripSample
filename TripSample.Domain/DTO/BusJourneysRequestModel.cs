using System.Text.Json.Serialization;
using TripSample.Domain.Model;

namespace TripSample.Domain.DTO
{
    public class BusJourneysRequestModel
    {
        [JsonPropertyName("device-session")]
        public required SessionModel SessionData { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("data")]
        public BusJourneyDataModel Data { get; set; }

    }
}
