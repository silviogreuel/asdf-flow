using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Controllers
{
    [Route("profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        [Route("")]
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