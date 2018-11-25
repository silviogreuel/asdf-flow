using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asdf.Domain.Users;
using Serilog;

namespace Asdf.Domain.Actions
{
    public class GuardNode : Node
    {
        public string Field { get; set; }

        public GuardNode() { }

        public GuardNode(User user, string name, string field) : base(user, name)
        {
            this.Field = field;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            bool pass = context.ContainsKey(Field);
            if (pass)
                await NextPassAsync(context);
            else
                await NextFailAsync(context);
        }
    }
}
