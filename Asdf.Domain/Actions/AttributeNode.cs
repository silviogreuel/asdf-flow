using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asdf.Domain.Actions
{
    public class AttributeNode : Node
    {
        public IDictionary<string, dynamic> Attributes { get; set; }

        public AttributeNode() { }

        public AttributeNode(string name, IDictionary<string, dynamic> attributes) : base(name)
        {
            this.Attributes = attributes;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            foreach (var attribute in Attributes.ToList())
            {
                context[attribute.Key] = attribute.Value;
            }
            await NextPassAsync(context);
        }
    }
}
