namespace TripSample.Domain
{
    public class BaseApiResponse
    {
        public string Status { get; set; }
        public string UserMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
