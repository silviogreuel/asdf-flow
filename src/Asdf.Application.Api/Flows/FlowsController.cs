using System.Linq;
using System.Threading.Tasks;
using Asdf.Application.Api.Devices;
using Asdf.Application.Api.Extensions;
using Asdf.Application.Api.Shared;
using Asdf.Application.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Api.Flows
{
    [Route("api/flows")]
    [ApiController]
    public class FlowsController : AuthorizeController
    {
        private readonly AsdfContext db;

        public FlowsController(AsdfContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> ListFlows()
        {
            var flows = await db.Flows.Where(d => d.User.Id == User.GetUserId()).ToListAsync();
            return Ok(new
            {
                flows = flows.Select(f => new {f.Id, f.Name})
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlow([FromBody]CreateFlowRequest request)
        {
            var user = await db.Users.FindAsync(User.GetUserId());
            var entry = await db.Flows.AddAsync(new Domain.Flows.Flow(user, request.Name));
            await db.SaveChangesAsync();
            return Ok(new { id = entry.Entity.Id });
        }

        [Route("{id}"), HttpDelete]
        public async Task<IActionResult> DeleteFlow([FromRoute]long id)
        {
            var flow = await db.Flows.FirstAsync(d => d.User.Id == User.GetUserId());
            db.Flows.Remove(flow);
            await db.SaveChangesAsync();
            return Ok(new {ok= true});
        }
    }

    public class CreateFlowRequest
    {
        public string Name { get; set; }
    }
}