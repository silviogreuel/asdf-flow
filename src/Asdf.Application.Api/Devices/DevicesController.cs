using System.Linq;
using Asdf.Application.Api.Shared;
using Asdf.Application.Database;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Asdf.Application.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Asdf.Application.Api.Devices
{
    [Route("api/devices")]
    [ApiController]
    public partial class DevicesController : AuthorizeController
    {
        private readonly AsdfContext db;

        public DevicesController(AsdfContext db) => this.db = db;

        [HttpPost]
        public async Task<CreateDeviceResponse> CreateDevice([FromBody]CreateDeviceRequest request)
        {
            var user = await db.Users.FindAsync(User.GetUserId());
            var entry = await db.Devices.AddAsync(new Domain.Devices.Device(user, request.Name));
            await db.SaveChangesAsync();
            return new CreateDeviceResponse(entry.Entity.Id);
        }

        [Route("{id}"), HttpDelete]
        public async Task<IActionResult> DelteDevice([FromRoute]long id)
        {
            var device = await db.Devices.FirstAsync(d => d.User.Id == User.GetUserId());
            db.Devices.Remove(device);
            await db.SaveChangesAsync();
            return Ok(new {ok= true});
        }

        [HttpGet]
        public async Task<ListDeviceResponse> ListDevices()
        {
            var devices = await db.Devices.Where(d => d.User.Id == User.GetUserId()).ToListAsync();
            return new ListDeviceResponse()
            {
                Devices = devices
            };
        }
    }
}