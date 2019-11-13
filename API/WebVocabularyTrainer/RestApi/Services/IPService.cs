using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public static class IPService
    {
        public static string GetSenderIPAddress(Controller controller) => controller?.Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }
}
