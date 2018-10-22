using System.Linq;
using Asdf.Application.Api.Shared;
using Asdf.Application.Database;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
            var user = await db.Users.FindAsync(request.UserId);
            var entry = await db.Devices.AddAsync(new Domain.Devices.Device(user, request.Name));
            await db.SaveChangesAsync();
            return new CreateDeviceResponse(entry.Entity.Id);
        }

        [HttpPut]
        public async Task<UpdateDeviceResponse> UpdateDevice([FromBody]UpdateDeviceRequest request)
        {
            var device = await db.Devices.FindAsync(request.Id);
            device.Name = request.Name;
            db.Devices.Update(device);
            await db.SaveChangesAsync();
            return new UpdateDeviceResponse();
        }

        [HttpGet]
        public async Task<ListDeviceResponse> ListDevices()
        {
            var devices = await db.Devices.ToListAsync();
            return new ListDeviceResponse()
            {
                Devices = devices
            };
        }
    }
}