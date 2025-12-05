using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TripSample.Domain.DTO
{
    public class BusJourneyDataModel
    {
        
        [JsonPropertyName("origin-id")]
        public int OriginId { get; set; }
        [JsonPropertyName("destination-id")]
        public int TargetId { get; set; }

        private string _departureDate;

        [JsonPropertyName("departure-date")]
        public string DepartureDate {
            get => _departureDate;
            set
            {
                if (DateTime.TryParse(value, out var dt))
                {
                    _departureDate = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    _departureDate = value;
                }
            }

        }

        public string OriginName { get; set; }
        public string TargetName { get; set; }

    }
}
