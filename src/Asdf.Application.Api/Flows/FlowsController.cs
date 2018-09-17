using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Flows
{
    [Route("api/flows")]
    [ApiController]
    [Authorize]
    public class FlowsController : ControllerBase
    {
    }
}