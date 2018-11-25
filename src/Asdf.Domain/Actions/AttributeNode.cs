using System.Collections.Generic;
using System.Threading.Tasks;
using Asdf.Domain.Users;
using Asdf.Kernel.Utils;
using Serilog;

namespace Asdf.Domain.Actions
{
    public class AttributeNode : Node
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public AttributeNode() { }

        public AttributeNode(User user, string name, string key, string value, string type) : base(user, name)
        {
            this.Key = key;
            this.Value = value;
            this.Type = type;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            context[Key] = Type.GetDotNetValue(Value);
            Log.Logger.Information($"ADD {Type}:{Key}:{Value}");
            await NextPassAsync(context);
        }
    }
}
