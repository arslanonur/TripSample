using Microsoft.Extensions.Caching.Memory;
using TripSample.Application.Interfaces;
using TripSample.Domain.DTO;
using TripSample.Domain.Model;
using TripSample.Infrastructure;
using TripSample.Infrastructure.Client;

namespace TripSample.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IObiletApiClient _obiletApiClient;


        public SessionService(IObiletApiClient obiletApiClient)
        {
            _obiletApiClient = obiletApiClient;
        }
        public async Task<SessionModel> GetSessionAsync(ConnectionModel connectionModel,BrowserModel browserModel)
        {
            var getSessionRequest = new GetSessionRequest
            {
                Connection = connectionModel,
                Browser = browserModel,
                Type = 1
            };

            var getResponse = await _obiletApiClient.PostAsync<GetSessionRequest, GetSessionResponse>(Endpoints.GetSession, getSessionRequest);

            if (getResponse.Status != "Success")
            {
                throw new Exception("Session Oluşturulamadı");
            }
            else if (getResponse.Data != null)
            {
                return new SessionModel { DeviceId = getResponse.Data.DeviceId, SessionId = getResponse.Data.SessionId };
            }

            return null;
        }


    }
}
