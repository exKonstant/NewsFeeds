using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.Authentication;
using NewsFeeds.Authentication.Services;

namespace NewsFeeds.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
       
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginModel model)
        {
            var logInSuccess = await _authenticationService.LogIn(model.Email, model.Password);
            if (!logInSuccess)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _authenticationService.LogOut();
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var identityErrors =
                await _authenticationService.Register(model.Email, model.Password, model.PasswordConfirm, model.Roles);
            if (identityErrors.Count() != 0)
            {
                return BadRequest(identityErrors);
            }
            return Ok();
        }
    }
}
