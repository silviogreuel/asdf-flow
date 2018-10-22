using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Asdf.Domain.Users;
using Asdf.Kernel.Utils;

namespace Asdf.Domain.Actions
{
    public class MqttPublishNode : Node
    {
        public Guid Device { get; set; }
        public string Field { get; set; }

        public MqttPublishNode() { }

        public MqttPublishNode(User user, string name, string device, string field) : base(user, name)
        {
            this.Device = Guid.Parse(device);
            this.Field = field;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            var queue = $"mqtt-subscription-{Device}qos0";
            var payload = Encoding.UTF8.GetBytes(context[Field]);
            await GlobalBus.Publish(string.Empty, queue, payload);
        }
    }
}