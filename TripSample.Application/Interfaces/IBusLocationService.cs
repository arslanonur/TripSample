using TripSample.Domain.DTO;
using TripSample.Domain.Model;

namespace TripSample.Application.Interfaces
{
    public interface IBusLocationService
    {
        Task<List<BusLocationModel>> GetBusLocationsAsync(string data, SessionModel sessionInfo);

    }
}
