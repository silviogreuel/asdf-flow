using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Profile
{
    [Route("profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        [Route(""), HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(new
            {
                User.Identity.Name,
                User.Claims,
            });
        }
    }
}