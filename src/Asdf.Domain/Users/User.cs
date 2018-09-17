using Asdf.Kernel;
using System;

namespace Asdf.Domain.Users
{
    public class User : IHaveId<long?>
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Guid? Token { get; set; }

        public long? AuthId { get; set; }
        public string AuthProvider { get; set; }

        public User() { }

        public User(string name, string email)
            : this(name, email, Guid.NewGuid())
        { }

        public User(string name, string email, Guid token)
        {
            this.Name = name;
            this.Email = email;
            this.Token = token;
        }
    }
}
