namespace TripSample.Infrastructure
{
    public class ObiletApiOptions
    {
        public string BaseUrl { get; set; }
        public string ApiClientToken { get; set; }
        public string Language { get; set; } = "tr-TR";
        public string DeviceId { get; set; }
    }
}
