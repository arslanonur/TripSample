using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TripSample.Application.Interfaces;
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
        private const string _busLocationsCacheKey = "bus_locations_all";

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
                Language = "tr-TR"
            };


            if (_memoryCache.TryGetValue(_busLocationsCacheKey + "_" + data, out List<BusLocationModel> busLocationsAllFromCache))
            {
                return busLocationsAllFromCache;
            }

            var getResponse = await _obiletApiClient.PostAsync<BusLocationsRequestModel, BusLocationResponseModel>(Endpoints.GetBusLocations, busLocationReqeust);

            if (getResponse.Status != "Success")
            {
                throw new Exception("Konumlar getirilemedi!");
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

                _memoryCache.Set(_busLocationsCacheKey + "_" + data, busLocationListModel, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) });

                return busLocationListModel;
            }

            return null;

        }
    }
}
