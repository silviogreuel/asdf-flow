using System.Collections.Generic;
using Asdf.Domain.Devices;

namespace Asdf.Application.Api.Devices
{
    public partial class DevicesController
    {
        public class ListDeviceResponse
        {
            public List<Device> Devices { get; set; }
        }
    }
}