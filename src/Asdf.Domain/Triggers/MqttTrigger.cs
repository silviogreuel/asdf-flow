using System.Collections.Generic;
using Asdf.Domain.Users;

namespace Asdf.Domain.Triggers
{
    public class MqttTrigger : Trigger
    {
        public string Topic { get; set; }
        public MqttTrigger() { }

        public MqttTrigger(string topic, User user, string name) : base(user, name)
        {
            this.Topic = topic;
            this.Context = new Dictionary<string, dynamic>();
        }
    }
}