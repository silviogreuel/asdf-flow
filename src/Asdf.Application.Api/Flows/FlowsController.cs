using Asdf.Application.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Flows
{
    [Route("api/flows")]
    [ApiController]
    public class HealthController : AuthorizeController
    {
    }
}