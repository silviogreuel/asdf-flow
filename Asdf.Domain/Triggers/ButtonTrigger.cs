using System.Collections.Generic;

namespace Asdf.Domain.Triggers
{
    public class ButtonTrigger : Trigger
    {
        public ButtonTrigger() { }

        public ButtonTrigger(string name) : base(name)
        {
            this.Context = new Dictionary<string, dynamic>();
        }
    }
}
