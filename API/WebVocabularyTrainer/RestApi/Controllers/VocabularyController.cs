using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog;
using RestApi.Data.Models;
using RestApi.Interfaces;
using RestApi.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RestApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(AuthSchemes)]
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VocabularyController : Controller
    {
        private const string AuthSchemes = CookieAuthenticationDefaults.AuthenticationScheme + "," + JwtBearerDefaults.AuthenticationScheme;
        protected IVocabularyService _vocabularyService;
        protected Logger _logger;

        public VocabularyController(IVocabularyService vocabularyService)
        {
            _vocabularyService = vocabularyService;
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested all entries.");
            var result = await _vocabularyService.GetAsync().ConfigureAwait(false);
            if (result.IsFine)
            {
                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: {result.Output.Count()} entries / (200).");
                return Ok(result.Output);
                
            }
            _logger.Error(result.Exception, $"[{IPService.GetSenderIPAddress(this)}] Output: 0 entries / (500).");
            return StatusCode(500);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested entry by id: {id}.");
            var result = await _vocabularyService.GetAsync(id).ConfigureAwait(false);
            if (result.IsFine)
            {
                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: 1 entry / (200).");
                return Ok(result.Output);
            }
            _logger.Error(result.Exception, $"[{IPService.GetSenderIPAddress(this)}] Output: 0 entries / (500).");
            return StatusCode(500);
        }

        //https://stackoverflow.com/questions/14407458/webapi-multiple-put-post-parameters
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]JObject data)
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested to add new entry.");
            var newEntry = data.ToObject<Sentence>();
            var result = await _vocabularyService.AddAsync(newEntry).ConfigureAwait(false);
            if (result.IsFine)
            {
                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: ({result.Output}).");
                return StatusCode(result.Output);
            }
            _logger.Error($"[{IPService.GetSenderIPAddress(this)}] Output: {result.Exception.Message} / ({result.Output}).");
            return StatusCode(result.Output, result.Exception.Message);
        }

        [HttpPut]
        [Produces("application/xml")]
        public async Task<IActionResult> Update([FromBody]JsonElement data)
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested to update entry.");
            var entry = JsonConvert.DeserializeObject<Sentence>(data.ToString());
            var result = await _vocabularyService.UpdateAsync(entry).ConfigureAwait(false);
            if (result.IsFine)
            {
                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: ({result.Output}).");
                return StatusCode(result.Output);
            }
            _logger.Error($"[{IPService.GetSenderIPAddress(this)}] Output: {result.Exception.Message} / ({result.Output}).");
            return StatusCode(result.Output, result.Exception.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested to delete entry.");
            var result = await _vocabularyService.DeleteAsync(id).ConfigureAwait(false);
            if (result.IsFine)
            {
                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: ({result.Output}).");
                return StatusCode(result.Output);
            }
            _logger.Error($"[{IPService.GetSenderIPAddress(this)}] Output: {result.Exception.Message} / ({result.Output}).");
            return StatusCode(result.Output, result.Exception.Message);
        }
    }
}