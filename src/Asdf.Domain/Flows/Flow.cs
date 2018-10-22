using System;
using Asdf.Domain.Triggers;
using Asdf.Domain.Users;
using Asdf.Kernel;

namespace Asdf.Domain.Flows
{
    public class Flow : IHaveId<long?>
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public virtual Trigger Trigger { get; set; }
        public virtual User User { get; set; }

        public Flow() { }

        public Flow(User user, string name)
        {
            this.Name = name;
            this.User = user;
        }

        public void AddTrigger(Trigger trigger)
        {
            this.Trigger = trigger;
        }
    }
}
