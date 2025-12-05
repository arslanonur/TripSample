using System.Globalization;
using System.Text.Json.Serialization;

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
        public string DepartureDate
        {
            get => _departureDate;
            set
            {
                var culture = new CultureInfo(Const.Const.DefaultLanguage);

                if (DateTime.TryParseExact(value, "dd.MM.yyyy", culture,
                                           DateTimeStyles.None, out var dt))
                {
                    _departureDate = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
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
