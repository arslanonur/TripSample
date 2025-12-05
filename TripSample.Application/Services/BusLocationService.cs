using Microsoft.Extensions.Caching.Memory;
using TripSample.Application.Interfaces;
using TripSample.Domain.Const;
using TripSample.Domain.DTO;
using TripSample.Domain.Model;
using TripSample.Infrastructure;
using TripSample.Infrastructure.Client;

namespace TripSample.Application.Services
{
    public class BusLocationService : IBusLocationService
    {
        private readonly IObiletApiClient _obiletApiClient;
        private readonly IMemoryCache _memoryCache; //todo: onur => daha sonra redis e geçilebilir

        public BusLocationService(IObiletApiClient obiletApiClient, IMemoryCache memoryCache)
        {
            _obiletApiClient = obiletApiClient;
            _memoryCache = memoryCache;
        }
        public async Task<List<BusLocationModel>> GetBusLocationsAsync(string data, SessionModel sessionInfo)
        {
            var busLocationReqeust = new BusLocationsRequestModel
            {
                Data = data,
                SessionData = sessionInfo,
                Date = DateTime.Now,
                Language = Const.DefaultLanguage,
            };


            if (_memoryCache.TryGetValue(Const.BusLocationsCacheKey + "_" + data, out List<BusLocationModel> busLocationsAllFromCache))
            {
                return busLocationsAllFromCache;
            }

            var getResponse = await _obiletApiClient.PostAsync<BusLocationsRequestModel, BusLocationResponseModel>(Endpoints.GetBusLocations, busLocationReqeust);

            if (getResponse == null)
            {
                return null;
            }

            if (getResponse.Status != Const.SuccessStatus)
            {
                throw new Exception(Const.LocationsCannotCreated);
            }
            else if (getResponse.Data != null)
            {
                var busLocationListModel = new List<BusLocationModel>();

                foreach (var busLocation in getResponse.Data)
                {
                    busLocationListModel.Add(new BusLocationModel
                    {
                        Id = busLocation.Id,
                        Name = busLocation.Name,
                        ParentId = busLocation.ParentId,
                        Keywords = busLocation.Keywords,
                        CityName = busLocation.CityName,
                        LongName = busLocation.LongName,
                    });
                }

                _memoryCache.Set(Const.BusLocationsCacheKey + "_" + data, busLocationListModel, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });

                return busLocationListModel;
            }

            return null;

        }
    }
}
