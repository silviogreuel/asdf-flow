using Asdf.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Asdf.Kernel.Utils;
using Serilog;

namespace Asdf.Domain.Actions
{
    public class GpioNode : Node
    {
        public Guid Device { get; set; }
        public string Gpio { get; set; }
        public GpioStatusType Status { get; set; }

        public GpioNode() { }

        public GpioNode(User user, string name, string device, string gpio, string status) : base(user, name)
        {
            this.Device = Guid.Parse(device);
            this.Gpio = gpio;
            this.Status = Enum.Parse<GpioStatusType>(status, true);
        }

        public GpioNode(User user, string name, string device, string gpio, GpioStatusType status) : base(user, name)
        {
            this.Device = Guid.Parse(device);
            this.Gpio = gpio;
            this.Status = status;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            var exchange = "amq.topic";
            var queue = $"mqtt-subscription-{Device}qos0";
            var data = $"gpio:{Gpio}:{Status.ToString().ToLower()}";
            var payload = Encoding.UTF8.GetBytes(data);
            Log.Logger.Information($"GPIO {queue} {payload}");
            await GlobalBus.Publish(string.Empty, queue, payload);
        }
    }
}
