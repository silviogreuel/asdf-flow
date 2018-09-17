using System.Threading.Tasks;
using Asdf.Application.Database;
using Asdf.Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : ControllerBase
    {
        private readonly AsdfContext db;

        public AuthController(AsdfContext db) => this.db = db;

        [Route("signin/{provider}")]
        public IActionResult SignIn(string provider, string returnUrl = null) =>
            Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);

        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult(new { signout = true });
        }

        [Route("demo")]
        public async Task<IActionResult> Demo()
        {
            await db.Users.AddAsync(new User("demo", "demo@demo.com"));
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}