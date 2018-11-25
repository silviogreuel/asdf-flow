using System.Linq;
using System.Threading.Tasks;
using Asdf.Application.Api.Devices;
using Asdf.Application.Api.Extensions;
using Asdf.Application.Api.Shared;
using Asdf.Application.Database;
using Asdf.Domain.Templates;
using Asdf.Domain.Triggers;
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
            var flow = await db.Flows.FirstAsync(d => d.Id == id && d.User.Id == User.GetUserId());
            db.Flows.Remove(flow);
            await db.SaveChangesAsync();
            return Ok(new {ok= true});
        }

        [Route("{id}"), HttpGet]
        public async Task<IActionResult> GetFlow([FromRoute]long id)
        {
            var flow = await db.Flows.FirstAsync(d => d.Id == id && d.User.Id == User.GetUserId());
            return Ok(new {flow});
        }

        [Route("add-trigger"), HttpPost]
        public async Task<IActionResult> AddTrigger([FromBody]AddTriggerRequest request)
        {
            var user = await db.Users.FindAsync(User.GetUserId());
            var flow = await db.Flows.FirstAsync(d => d.Id == request.FlowId && d.User.Id == user.Id);

            Trigger trigger = null;
            switch (request.TriggerType)
            {
                case "Button":
                    trigger = new ButtonTrigger(user, request.TriggerName);
                    break;
                case "MQTT":
                    trigger = new MqttTrigger(request.TriggerTopic, user, request.TriggerName);
                    break;
            }

            if (flow.Trigger != null)
                db.Triggers.Remove(flow.Trigger);

            flow.AddTrigger(trigger);

            await db.SaveChangesAsync();
            return Ok(new {ok= true});
        }

        [Route("add-root")]
        public async Task<IActionResult> AddRoot(AddNextNodeRequest request)
        {
            var user = await db.Users.FindAsync(User.GetUserId());
            var trigger = await db.Triggers.FirstAsync(d => d.Id == request.NodeId && d.User.Id == user.Id);
            var values = request.Node.GetFields().Prepend(user).ToArray();
            var node = request.Node.Activate(values);

            trigger.AddRoot(node);
            db.Update(trigger);
            await db.SaveChangesAsync();
            return Ok(new {ok = true});
        }

        [Route("add-pass")]
        public async Task<IActionResult> AddPass(AddNextNodeRequest request)
        {
            var user = await db.Users.FindAsync(User.GetUserId());
            var node = await db.Nodes.FirstAsync(d => d.Id == request.NodeId && d.User.Id == user.Id);
            var values = request.Node.GetFields().Prepend(user).ToArray();
            var passNode = request.Node.Activate(values);

            node.AddPass(passNode);
            db.Update(node);
            await db.SaveChangesAsync();
            return Ok(new {ok = true});
        }

        [Route("add-fail")]
        public async Task<IActionResult> AddFail(AddNextNodeRequest request)
        {
            var user = await db.Users.FindAsync(User.GetUserId());
            var node = await db.Nodes.FirstAsync(d => d.Id == request.NodeId && d.User.Id == user.Id);
            var values = request.Node.GetFields().Prepend(user).ToArray();
            var passNode = request.Node.Activate(values);

            node.AddFail(passNode);
            db.Update(node);
            await db.SaveChangesAsync();
            return Ok(new {ok = true});
        }
    }
    public class AddNextNodeRequest
    {
        public long NodeId { get; set; }
        public NodeTemplate Node { get; set; }
    }

    public class AddTriggerRequest
    {
        public string TriggerName { get; set; }
        public string TriggerType { get; set; }
        public string TriggerTopic { get; set; }
        public long FlowId { get; set; }
    }

    public class CreateFlowRequest
    {
        public string Name { get; set; }
    }
}