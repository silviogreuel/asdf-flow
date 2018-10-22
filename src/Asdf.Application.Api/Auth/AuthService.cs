using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Asdf.Application.Database;
using Asdf.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace Asdf.Application.Api.Auth
{
    public interface IAuthService
    {
        Task<User> Register(string name, string authProvider);
        User Authenticate(string name, int pin);
    }

    public class AuthService : IAuthService
    {
        private readonly AsdfContext db;

        public AuthService(AsdfContext db)
        {
            this.db = db;
        }

        public async Task<User> Register(string name, string authProvider)
        {
            var user = db.Users.Where(u => u.Name == name && u.AuthProvider == authProvider).FirstOrDefault();

            if (user == null)
            {
                user = new User(name, email: null)
                {
                    AuthProvider = authProvider,
                };
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
            }

            return user;
        }

        public User Authenticate(string name, int pin)
        {
            var user = db.Users.SingleOrDefault(x => x.Name == name && x.Pin == pin);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("TLAYa3jcU8Hg8r6BEE2yY2vzrZLUO4rc");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.JwtToken = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
