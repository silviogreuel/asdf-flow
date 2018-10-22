using System.Security.Claims;
using Asdf.Application.Api.Extensions;
using Asdf.Application.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Profile
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private AsdfContext db;

        public ProfileController(AsdfContext db)
        {
            this.db = db;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var user = db.Users.Find(User.GetUserId());

            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                token = user.Token
            });
        }
    }
}