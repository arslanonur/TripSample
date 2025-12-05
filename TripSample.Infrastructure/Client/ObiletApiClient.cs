using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using TripSample.Domain;

namespace TripSample.Infrastructure.Client
{
    public class ObiletApiClient : IObiletApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ObiletApiOptions _options;

        public ObiletApiClient(HttpClient httpClient, IOptions<ObiletApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;

            _httpClient.BaseAddress = new Uri(_options.BaseUrl);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", _options.ApiClientToken);

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await _httpClient.PostAsync(_httpClient.BaseAddress + endpoint, content);

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"Obilet API Error: {result.StatusCode}");
                }

                var responseJson = await result.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<TResponse>(responseJson);

                if (response == null)
                    throw new Exception("API Response is null");

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                return JsonSerializer.Deserialize<TResponse>(string.Empty);
            }
            
        }
    }
}
