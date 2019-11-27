using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

namespace RestApi.Controllers
{
    public class GameController : Controller
    {
        protected Logger _logger;
        public GameController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}