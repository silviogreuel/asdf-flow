﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Asdf.Application.Database;
using DomainUser = Asdf.Domain.Users.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Auth
{
    [Route("auth")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService auth;

        public AuthController(IAuthService auth)
        {
            this.auth = auth;
        }

        [Route("signin/{provider}")]
        public IActionResult SignInProvider(string provider, string returnUrl = null) =>
            Challenge(new AuthenticationProperties { RedirectUri = "http://localhost:8081/account" }, provider);

        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { signout = true });
        }

        [Route("signin")]
        [Authorize]
        public async Task<IActionResult> SignIn()
        {
            var name = this.User.Identity.Name;
            var authProvider = this.User.Identity.AuthenticationType;

            var user = auth.Register(name, authProvider);            
            
            return Ok(user);
        }

        public class LoginRequest
        {
            public string  User { get; set; }
            public int Pin { get; set; }
        }

        [Route("authenticate"), HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody]LoginRequest login)
        {
            var user = auth.Authenticate(login.User, login.Pin);

            if (user == null)
                return BadRequest(new { message = "Username or pin is incorrect" });

            return Ok(user);
        }

        [HttpGet("check")]
        [Authorize]
        public IActionResult GetAll()
        {
            return new JsonResult(new {ok = true});
        }
    }
}