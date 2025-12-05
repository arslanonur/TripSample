using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripSample.Domain.DTO;
using TripSample.Domain.Model;

namespace TripSample.Application.Interfaces
{
    public interface ISessionService
    {
        /// <summary>
        /// API de kullanılacak olan session bilgilerini oluşturan method.
        /// </summary>
        /// <param name="connectionModel"></param>
        /// <param name="browserModel"></param>
        /// <returns></returns>
        Task<SessionModel> GetSessionAsync(ConnectionModel connectionModel, BrowserModel browserModel); 
    }
}
