using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asdf.Domain.Actions;
using Asdf.Domain.Users;
using Asdf.Kernel;

namespace Asdf.Domain.Triggers
{
    public class Trigger : IHaveId<long?>, IHaveContext, ICanBeExecutable
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public virtual Node Root { get; set; }
        public virtual User User { get; set; }
        public virtual IDictionary<string, dynamic> Context { get; set; }

        public Trigger() { }

        public Trigger(User user, string name)
        {
            this.Name = name;
            this.User = user;
        }

        public async Task ExecuteAsync()
        {
            if (Root == null) { return; }
            await Root.ExecuteAsync(Context);
        }

        public void AddRoot(Node root)
        {
            Root = root;
        }
    }
}
