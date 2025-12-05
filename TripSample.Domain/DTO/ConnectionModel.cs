using System.Text.Json.Serialization;

namespace TripSample.Domain.DTO
{
    public class ConnectionModel
    {
        [JsonPropertyName("ip-address")]
        public string IpAdress { get; set; }
        [JsonPropertyName("port")]
        public string Port { get; set; }
    }
}
