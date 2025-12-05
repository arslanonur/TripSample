using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripSample.Domain.DTO;

namespace TripSample.Application.Interfaces
{
    public interface IJourneyService
    {
        Task<BusJourneysResponseModel> GetBusJourneyAsync(BusJourneysRequestModel busJourneysRequestModel);
    }
}
