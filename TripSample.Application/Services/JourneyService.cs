using Microsoft.Extensions.Caching.Memory;
using TripSample.Application.Interfaces;
using TripSample.Domain.Const;
using TripSample.Domain.DTO;
using TripSample.Infrastructure;
using TripSample.Infrastructure.Client;

namespace TripSample.Application.Services
{
    public class JourneyService : IJourneyService
    {
       
        private readonly IObiletApiClient _obiletApiClient;
        private readonly IMemoryCache _memoryCache; //todo: onur => daha sonra redis e geçilebilir

        public JourneyService(IObiletApiClient obiletApiClient, IMemoryCache memoryCache)
        { 
            _obiletApiClient = obiletApiClient;
            _memoryCache = memoryCache;
        }
        public async Task<BusJourneysResponseModel> GetBusJourneyAsync(BusJourneysRequestModel busJourneysRequestModel)
        {
            busJourneysRequestModel.Language = Const.DefaultLanguage;
            busJourneysRequestModel.Date = DateTime.Now;

            var busJourneysFromCache = GetJourneysFromCache(busJourneysRequestModel.Data.TargetId, busJourneysRequestModel.Data.OriginId, busJourneysRequestModel.Data.DepartureDate);
            if (busJourneysFromCache != null)
            {
                return busJourneysFromCache;
            }

            var getResponse = await _obiletApiClient.PostAsync<BusJourneysRequestModel, BusJourneysResponseModel>(Endpoints.GetJourneys, busJourneysRequestModel);
            if (getResponse.Status != Const.SuccessStatus)
            {
                throw new Exception(Const.JourneysCannotCreated);
            }
            else if (getResponse.Data != null)
            {
                getResponse.Data = getResponse.Data.OrderBy(x => x.Journey.Departure).ToList();

                _memoryCache.Set(Const.BusJourneysCacheKey + busJourneysRequestModel.Data.TargetId + "_" + busJourneysRequestModel.Data.OriginId + "_" + busJourneysRequestModel.Data.DepartureDate, getResponse, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });

                return getResponse;
            }

            return null;
        }

        private BusJourneysResponseModel GetJourneysFromCache(int targetId, int originId, string departureDate)
        {
            var cacheKey = Const.BusJourneysCacheKey + targetId + "_" + originId + "_" + departureDate;
            _memoryCache.TryGetValue(cacheKey, out BusJourneysResponseModel busJourneysFromCache);
            return busJourneysFromCache;
        }
    }
}
