using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using RestApi.Services;

namespace RestApi.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        protected Logger _logger;
        public GameController(GameService gameService)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _gameService = gameService;
        }


        public async Task<IActionResult> GetSettingsForm()
        {
            var output = await _gameService.GetSettingsForm();
            return View();
        }
    }
}