using Asdf.Application.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Actions
{
    [Route("api/actions")]
    [ApiController]
    public class ActionsController : AuthorizeController
    {
    }
}