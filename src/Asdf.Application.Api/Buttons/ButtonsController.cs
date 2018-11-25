using System.Data.Odbc;
using System.Linq;
using Asdf.Application.Api.Shared;
using Asdf.Application.Database;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Asdf.Application.Api.Extensions;
using Asdf.Domain.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Api.Buttons
{
    [Route("api/buttons")]
    [ApiController]
    public partial class ButtonsController : AuthorizeController
    {
        private readonly AsdfContext db;

        public ButtonsController(AsdfContext db) => this.db = db;

        [Route("{id}/trigger"), HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteDevice([FromRoute]long id)
        {
            var flows = await db.Flows.Where(f => f.Trigger.Id == id && f.User.Id == User.GetUserId()).ToListAsync();
            var buttons = flows.Select(f => f.Trigger).ToList();

            foreach (var button in buttons)
            {
                await button.ExecuteAsync();
            }

            db.UpdateRange(buttons);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListButtons()
        {
            var flows = await db.Flows.Where(f => f.User.Id == User.GetUserId()).ToListAsync();
            var triggers = flows.Select(f => f.Trigger).ToList();
            var buttons = triggers.Where(b => b is ButtonTrigger).ToList();

            return Ok(new
            {
                buttons = buttons.Select(b => new { b.Id, b.Name }).ToList()
            });
        }
    }
}