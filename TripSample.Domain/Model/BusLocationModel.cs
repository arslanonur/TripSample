using System.Text.Json.Serialization;

namespace TripSample.Domain.Model
{
    public class BusLocationModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("parent-id")]
        public int ParentId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("geo-location")]
        public GeoLocationModel GeoLocation { get; set; }

        [JsonPropertyName("zoom")]
        public int Zoom { get; set; }

        [JsonPropertyName("tz-code")]
        public string TzCode { get; set; }

        [JsonPropertyName("weather-code")]
        public string WeatherCode { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("reference-code")]
        public string ReferenceCode { get; set; }

        [JsonPropertyName("city-id")]
        public int CityId { get; set; }

        [JsonPropertyName("reference-country")]
        public string ReferenceCountry { get; set; }

        [JsonPropertyName("country-id")]
        public int CountryId { get; set; }

        [JsonPropertyName("keywords")]
        public string Keywords { get; set; }

        [JsonPropertyName("city-name")]
        public string CityName { get; set; }

        [JsonPropertyName("languages")]
        public string Languages { get; set; }

        [JsonPropertyName("country-name")]
        public string CountryName { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("show-country")]
        public bool ShowCountry { get; set; }

        [JsonPropertyName("area-code")]
        public string AreaCode { get; set; }

        [JsonPropertyName("long-name")]
        public string LongName { get; set; }

        [JsonPropertyName("is-city-center")]
        public bool IsCityCenter { get; set; }

        [JsonPropertyName("seo-location-name")]
        public string SeoLocationName { get; set; }

        [JsonPropertyName("provider-location-id")]
        public string ProviderLocationId { get; set; }

        [JsonPropertyName("ufi-id")]
        public string UfiId { get; set; }

        [JsonPropertyName("is-domestic")]
        public bool IsDomestic { get; set; }

        [JsonPropertyName("current-location-id")]
        public int CurrentLocationId { get; set; }
    }
    
}
