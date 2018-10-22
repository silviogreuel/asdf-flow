using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Health
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("check")]
        public object GetCheck()
        {
            return new {ok = true};
        }
    }
}