using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using RestApi.Services;
using RestApi.ViewModels;

namespace RestApi.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public class AuthController : Controller
    //{
    //    protected UserManager<IdentityUser> _userManager;
    //    protected SignInManager<IdentityUser> _singInManager;
    //    private readonly AuthenticatorTokenProvider<IdentityUser> _tokenManager;
    //    protected Logger _logger;

    //    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> singInManager, 
    //        AuthenticatorTokenProvider<IdentityUser> tokenManager)
    //    {
    //        _userManager = userManager;
    //        _singInManager = singInManager;
    //        _tokenManager = tokenManager;
    //        _logger = LogManager.GetCurrentClassLogger();
    //    }

    //    [HttpPost("register")]
    //    public async Task<IActionResult> CreateUser([FromBody]JsonElement data)
    //    {
    //        _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested to add new user.");
    //        UserViewModel viewModel = null;
    //        try
    //        {
    //            viewModel = JsonConvert.DeserializeObject<UserViewModel>(data.ToString());
    //        }
    //        catch(Exception e)
    //        {
    //            _logger.Error(e, $"[{IPService.GetSenderIPAddress(this)}] Output: Unprocessable entity / (422).");
    //            return StatusCode(422, "Unprocessable entity.");
    //        }

    //        if (string.IsNullOrEmpty(viewModel.Login) || string.IsNullOrEmpty(viewModel.Password))
    //        {
    //            _logger.Error($"[{IPService.GetSenderIPAddress(this)}] Output: Login or password is empty / (400).");
    //            return BadRequest("Login or password is empty.");
    //        }

    //        var user = new IdentityUser(viewModel.Login);
    //        try
    //        {
    //            var result = await _userManager.CreateAsync(user, viewModel.Password);
    //            if (result.Succeeded)
    //            {
    //                _logger.Info($"[{IPService.GetSenderIPAddress(this)}] Output: User {user.UserName} created / (200).");
    //                return Ok($"User {user.UserName} created.");
    //            }
    //            else
    //            {
    //                var builder = new StringBuilder();
    //                foreach (var error in result.Errors)
    //                {
    //                    builder.AppendLine(error.Description);
    //                }
    //                return BadRequest(builder.ToString());
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            _logger.Error(e, $"[{IPService.GetSenderIPAddress(this)}] Output: Server error / (500).");
    //            return StatusCode(500, "Server error");
    //        }
    //    }

    //    [HttpPost("login")]
    //    [Produces("application/json")]
    //    public async Task<IActionResult> Authenticate([FromBody]JsonElement data, [FromServices] TokenService tokenService)
    //    {
    //        //_logger.Info($"[{IPService.GetSenderIPAddress(this)}] Requested to add new user.");
    //        UserViewModel viewModel = null;
    //        try
    //        {
    //            viewModel = JsonConvert.DeserializeObject<UserViewModel>(data.ToString());
    //        }
    //        catch(Exception e)
    //        {
    //            _logger.Error(e, $"[{IPService.GetSenderIPAddress(this)}] Output: Unprocessable entity / (422).");
    //            return StatusCode(422, "Unprocessable entity.");
    //        }

    //        if (string.IsNullOrEmpty(viewModel.Login) || string.IsNullOrEmpty(viewModel.Password))
    //        {
    //            return BadRequest("Login or password is empty.");
    //        }

    //        var result = await _singInManager.PasswordSignInAsync(viewModel.Login, viewModel.Password, false, false);
    //        if (!result.Succeeded)
    //        {
    //            return BadRequest("Login or password is incorrect.");
    //        }

    //        var user = await _userManager.FindByNameAsync(viewModel.Login);

    //        if (user == null || !(await _userManager.CheckPasswordAsync(user, viewModel.Password)))
    //        {
    //            return Unauthorized();
    //        }

    //        var token = tokenService.GenerateToken(user);

    //        HttpContext.Session.SetString("JWToken", token);
    //        //var authClaims = new[]
    //        //{
    //        //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
    //        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    //        //};

    //        //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OhiUWkHQ71deDRBf13So5ULBOVtf1vX5"));

    //        //var token = new JwtSecurityToken(
    //        //    issuer: "localhost",
    //        //    audience: "localhost",
    //        //    expires: DateTime.Now.AddHours(3),
    //        //    claims: authClaims,
    //        //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
    //        //    );

    //        //var outputToken = new JwtSecurityTokenHandler().WriteToken(token);
    //        //var expiriation = token.ValidTo;

    //        return Ok(token);
    //    }
    //}
}