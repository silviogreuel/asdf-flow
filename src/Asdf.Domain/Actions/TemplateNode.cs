using System.Collections.Generic;
using System.Threading.Tasks;
using Asdf.Domain.Users;

namespace Asdf.Domain.Actions
{
    public class TemplateNode : Node
    {
        public string Field { get; set; }
        public string Template { get; set; }

        public TemplateNode() { }

        public TemplateNode(User user, string name, string field, string template) : base(user, name)
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
