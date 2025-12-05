using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TripSample.Application.Interfaces;
using TripSample.Domain.Const;
using TripSample.Domain.DTO;
using TripSample.Domain.Model;

namespace TripSample.WebUI.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IBusLocationService _busLocationService;
        private readonly IJourneyService _journeyService;

        public HomeController(ISessionService sessionService, IBusLocationService busLocationService, IJourneyService journeyService)
        {
            _sessionService = sessionService;
            _busLocationService = busLocationService;
            _journeyService = journeyService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<BusLocationModel>> GetBusLocations(string q)
        {
            return await GetBusLocationsAsync(q);
        }

        public async Task<SessionModel> GetSession()
        {

            var sessionInfoFromCookie = GetSessionInfoFromCookie();
            if (sessionInfoFromCookie != null)
            {
                return sessionInfoFromCookie;
            }

            var sessionInfo = await _sessionService.GetSessionAsync(GetConnectionModel(), GetBrowserModel());

            if (sessionInfo != null)
            {
                AddCookieForSessionInfo(sessionInfo);
                return sessionInfo;
            }

            return null;

        }

        public async Task<List<BusLocationModel>> GetBusLocationsAsync(string data)
        {
            var sessionInfo = await GetSession();
            if (sessionInfo != null)
            {
                return await _busLocationService.GetBusLocationsAsync(data, sessionInfo);
            }

            return null;
        }

        [NullCheck]
        public async Task<IActionResult> JourneyIndex(BusJourneyDataModel dataModel)
        {
            var sessionInfo = await GetSession();
            if (sessionInfo != null)
            {
                var busJourneyRequestModel = new BusJourneysRequestModel
                {
                    SessionData = sessionInfo,
                    Data = new BusJourneyDataModel
                    {
                        OriginId = dataModel.OriginId,
                        TargetId = dataModel.TargetId,
                        DepartureDate = dataModel.DepartureDate,
                        OriginName = dataModel.OriginName,
                        TargetName = dataModel.TargetName
                    }
                };

                Response.Cookies.Append(Const.IndexOriginVal, dataModel.OriginId.ToString());
                Response.Cookies.Append(Const.IndexOriginName, dataModel.OriginName?.ToString());
                Response.Cookies.Append(Const.IndexTargetVal, dataModel.TargetId.ToString());
                Response.Cookies.Append(Const.IndexTargetName, dataModel.TargetName?.ToString());
                Response.Cookies.Append(Const.IndexDepartureVal, dataModel.DepartureDate);

                var journeyData = await _journeyService.GetBusJourneyAsync(busJourneyRequestModel);
                
                return View(journeyData);
            }
            return View();
        }

        #region Private Methods

        private void AddCookieForSessionInfo(SessionModel sessionModel)
        {
            Response.Cookies.Append(Const.TripSessionId, sessionModel.SessionId, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(5),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            Response.Cookies.Append(Const.TripDeviceId, sessionModel.DeviceId, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(5),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

        }

        private SessionModel GetSessionInfoFromCookie()
        {
            Request.Cookies.TryGetValue(Const.TripSessionId, out var tripSessionId);
            Request.Cookies.TryGetValue(Const.TripDeviceId, out var tripDeviceId);

            if (!string.IsNullOrEmpty(tripSessionId) && !string.IsNullOrEmpty(tripDeviceId))
            {
                return new SessionModel
                {
                    SessionId = tripSessionId,
                    DeviceId = tripDeviceId
                };
            }

            return null;
        }

        private ConnectionModel GetConnectionModel()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            return new ConnectionModel
            {
                IpAdress = ipAddress,
                Port = "8080"
            };
        }

        private BrowserModel GetBrowserModel()
        {
            var browserInfo = HttpContext.Request.Headers["sec-ch-ua"].ToString().Split(',');
            return new BrowserModel
            {
                Name = browserInfo[0].Split(';')[1].ToString().Replace("\"", string.Empty),
                Version = browserInfo[1].Split(';')[1].ToString().Replace("\"", string.Empty)
            };
        }

        #endregion
    }
}
