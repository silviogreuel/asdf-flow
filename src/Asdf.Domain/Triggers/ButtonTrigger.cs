using System.Collections.Generic;
using Asdf.Domain.Users;

namespace Asdf.Domain.Triggers
{
    public class ButtonTrigger : Trigger
    {
        public ButtonTrigger() { }

        public ButtonTrigger(User user, string name) : base(user, name)
        {
            this.Context = new Dictionary<string, dynamic>();
        }
    }
}
