using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using RestApi.Services;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        protected Logger _logger;
        public GameController(GameService gameService)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSettingsForm()
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested settings form.");
            var result = await _gameService.GetSettingsForm().ConfigureAwait(false);
            if (result.IsFine)
            {
                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: default settings form / (200).");
                return Ok(result.Output);
            }
            _logger.Error(result.Exception, $"[{IPService.GetSenderIPAddress(this)}] Output: {result.Exception.Message} / (500).");
            return StatusCode(500);
        }

        public async Task<IActionResult> Perform()
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested settings form.");
            var result = await _gameService.GetSettingsForm().ConfigureAwait(false);
            if (result.IsFine)
            {
                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: default settings form / (200).");
                return Ok(result.Output);
            }
            _logger.Error(result.Exception, $"[{IPService.GetSenderIPAddress(this)}] Output: {result.Exception.Message} / (500).");
            return StatusCode(500);
        }
    }
}