using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asdf.Domain.Users;

namespace Asdf.Domain.Actions
{
    public class AttributeNode : Node
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public AttributeNode() { }

        public AttributeNode(User user, string name, string key, string value) : base(user, name)
        {
            this.Key = key;
            this.Value = value;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            context[Key] = Value;
            await NextPassAsync(context);
        }
    }
}
