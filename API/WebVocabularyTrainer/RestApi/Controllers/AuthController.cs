using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;
using RestApi.Services;
using RestApi.ViewModels;

namespace RestApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        protected UserManager<IdentityUser> _userManager;
        protected SignInManager<IdentityUser> _singInManager;
        protected Logger _logger;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManager)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]JObject data)
        {
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested to add new user.");
            UserViewModel viewModel = null;
            try
            {
                viewModel = data.ToObject<UserViewModel>();
            }
            catch(Exception e)
            {
                _logger.Error(e, $"[{IPService.GetSenderIPAddress(this)}] Output: Unprocessable entity / (422).");
                return StatusCode(422, "Unprocessable entity");
            }
            
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(viewModel.Login);
                try
                {
                    var result = await _userManager.CreateAsync(user, viewModel.Password);
                    if (result.Succeeded)
                    {
                        _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: User {user.UserName} created / (200).");
                        return Ok($"User {user.UserName} created.");
                    }
                    else
                    {
                        var builder = new StringBuilder();
                        foreach (var error in result.Errors)
                        {
                            builder.Append($"{error.Description} ");
                        }
                        return BadRequest(builder.ToString());
                    }
                }
                catch(Exception e)
                {
                    _logger.Error(e, $"[{IPService.GetSenderIPAddress(this)}] Output: Server error / (500).");
                    return StatusCode(500, "Server error");
                }
            }
            _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: ModelState is invalid / (400).");
            return BadRequest("ModelState is invalid.");
        }
    }
}