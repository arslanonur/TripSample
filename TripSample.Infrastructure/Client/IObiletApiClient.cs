using TripSample.Domain;

namespace TripSample.Infrastructure.Client
{
    public interface IObiletApiClient
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
    }
}
