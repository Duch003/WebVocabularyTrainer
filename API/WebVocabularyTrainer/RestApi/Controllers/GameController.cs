using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using RestApi.Data.Models;
using RestApi.Services;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        private readonly VocabularyService _vocabularyService;
        protected Logger _logger;
        public GameController(GameService gameService, VocabularyService vocabularyService)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _gameService = gameService;
            _vocabularyService = vocabularyService;
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

        public async Task<IActionResult> Perform([FromBody]JsonElement data)
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested game based on settings.");
            var settings = JsonConvert.DeserializeObject<Settings>(data.ToString());
            
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