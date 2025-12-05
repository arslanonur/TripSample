using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TripSample.Domain.Model
{
    public class SessionModel
    {
        [JsonPropertyName("session-id")]
        public string SessionId { get; set; }
        [JsonPropertyName("device-id")]
        public string DeviceId { get; set; }
    }
}
