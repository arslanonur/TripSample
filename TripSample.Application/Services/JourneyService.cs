using Microsoft.Extensions.Caching.Memory;
using TripSample.Application.Interfaces;
using TripSample.Domain.DTO;
using TripSample.Infrastructure;
using TripSample.Infrastructure.Client;

namespace TripSample.Application.Services
{
    public class JourneyService : IJourneyService
    {
        private const string _busJourneysCacheKey = "bus_journeys_";
        private readonly IObiletApiClient _obiletApiClient;
        private readonly IMemoryCache _memoryCache; //todo: onur => daha sonra redis e geçilebilir

        public JourneyService(IObiletApiClient obiletApiClient, IMemoryCache memoryCache)
        { 
            _obiletApiClient = obiletApiClient;
            _memoryCache = memoryCache;
        }
        public async Task<BusJourneysResponseModel> GetBusJourneyAsync(BusJourneysRequestModel busJourneysRequestModel)
        {
            busJourneysRequestModel.Language = "tr-TR";
            busJourneysRequestModel.Date = DateTime.Now;

            if (_memoryCache.TryGetValue(_busJourneysCacheKey + busJourneysRequestModel.Data.TargetId + "_" + busJourneysRequestModel.Data.OriginId + "_" + busJourneysRequestModel.Data.DepartureDate, out BusJourneysResponseModel busJourneysFromCache))
            {
                return busJourneysFromCache;
            }


            var getResponse = await _obiletApiClient.PostAsync<BusJourneysRequestModel, BusJourneysResponseModel>(Endpoints.GetJourneys, busJourneysRequestModel);
            if (getResponse.Status != "Success")
            {
                throw new Exception("Seferler getirilemedi!");
            }
            else if (getResponse.Data != null)
            {
                _memoryCache.Set(_busJourneysCacheKey + busJourneysRequestModel.Data.TargetId + "_" + busJourneysRequestModel.Data.OriginId + "_" + busJourneysRequestModel.Data.DepartureDate, getResponse, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });

                return getResponse;

            }

            return null;

        }
    }
}
