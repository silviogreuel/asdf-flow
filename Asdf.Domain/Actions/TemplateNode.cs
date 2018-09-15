using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asdf.Domain.Actions
{
    public class TemplateNode : Node
    {
        public string Field { get; set; }
        public string Template { get; set; }

        public TemplateNode() { }

        public TemplateNode(string name, string field, string template) : base(name)
        {
            this.Field = field;
            this.Template = template;
        }

        public override async Task ExecuteAsync(IDictionary<string, dynamic> context)
        {
            context[Field] = string.Format(new TemplateFormatProvider(), Template, context);
            await NextPassAsync(context);
        }
    }
}
