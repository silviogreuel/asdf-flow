using Asdf.Kernel;
using System;

namespace Asdf.Domain.Users
{
    public class User : IHaveId<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Pin { get; set; }

        public Guid? Token { get; set; }

        public long? AuthId { get; set; }
        public string AuthProvider { get; set; }
        public string JwtToken { get; set; }

        public User() { }

        public User(string id, string name, string email)
            : this(id, name, email, Kernel.Utils.Pin.Generate(), Guid.NewGuid())
        { }

        public User(string id, string name, string email, int pin, Guid token)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Pin = pin;
            this.Token = token;
        }
    }
}
