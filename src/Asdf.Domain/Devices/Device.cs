using Asdf.Domain.Users;
using Asdf.Kernel;
using System;

namespace Asdf.Domain.Devices
{
    public class Device : IHaveId<long?>, IHaveName
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public Guid Token { get; set; }
        public virtual User User { get; set; }

        public Device() { }

        public Device(User user, string name, Guid token)
        {
            this.Name = name;
            this.User = user;
            this.Token = token;
        }

        public Device(User user, string name) 
            : this(user, name, Guid.NewGuid())
        { }
    }
}
