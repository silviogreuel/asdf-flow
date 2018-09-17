using Asdf.Application.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Asdf.Application.Api.Templates
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : AuthorizeController
    {
    }
}