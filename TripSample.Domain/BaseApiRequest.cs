using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripSample.Domain
{
    public class BaseApiRequest
    {
        public string Date { get; set; } = DateTime.Now.ToString("dd-MM-yyyy:mm:ss.fffZ");
        public string Language { get; set; } = "tr-TR";

        public ApiDevice Device { get; set; } = new ApiDevice();
        public ApiSession Session { get; set; } = new ApiSession();
    }

    public class ApiDevice
    {
        public int? Id { get; set; }
    }

    public class ApiSession
    {
        public string SessionId { get; set; }
    }
}
