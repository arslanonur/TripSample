using TripSample.Domain.DTO;
using TripSample.Domain.Model;

namespace TripSample.Application.Interfaces
{
    public interface IBusLocationService
    {
        /// <summary>
        /// API den otobüslerin lokasyonlarını dönen method
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sessionInfo"></param>
        /// <returns></returns>
        Task<List<BusLocationModel>> GetBusLocationsAsync(string data, SessionModel sessionInfo);

    }
}
