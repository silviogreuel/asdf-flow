using Asdf.Application.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Triggers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : AuthorizeController
    {
        [HttpGet("sample")]
        public object GetSample()
        {
            return new {data = "penis"};
        }
    }
}