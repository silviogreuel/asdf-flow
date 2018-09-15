using System;
using Asdf.Domain.Triggers;
using Asdf.Kernel;

namespace Asdf.Domain.Flows
{
    public class Flow : IHaveId<long?>
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public virtual Trigger Trigger { get; set; }

        public Flow() { }

        public Flow(string name)
        {
            this.Name = name;
        }

        public void AddTrigger(Trigger trigger)
        {
            this.Trigger = trigger;
        }
    }
}
