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
        /// <summary>
        /// API den seyahat detaylarını dönen method
        /// </summary>
        /// <param name="busJourneysRequestModel"></param>
        /// <returns></returns>
        Task<BusJourneysResponseModel> GetBusJourneyAsync(BusJourneysRequestModel busJourneysRequestModel);
    }
}
