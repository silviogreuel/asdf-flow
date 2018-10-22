using Asdf.Application.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Triggers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : AuthorizeController
    {
        //TODO: should be a template to
        [HttpGet]
        public IActionResult GetTriggers()
        {
            return Ok(new {
                triggers = new []
                {
                    "Button",
                    "MQTT"
                }});
        }
    }
}