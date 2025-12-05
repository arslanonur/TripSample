using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TripSample.Domain.Model;

namespace TripSample.Domain.DTO
{
    public class BusLocationsRequestModel
    {
        [JsonPropertyName("data")]
        public required string Data { get; set; }
        [JsonPropertyName("device-session")]
        public required SessionModel SessionData { get; set; }
        [JsonPropertyName("date")]
        public required DateTime Date { get; set; }
        [JsonPropertyName("language")]
        public required string Language { get; set; }
    }
}
