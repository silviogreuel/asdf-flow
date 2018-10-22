using System.Security.Claims;

namespace Asdf.Application.Api.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.Identity.Name;
        }

        public static string GetAuthenticationType(this ClaimsPrincipal principal)
        {
            return principal.Identity.AuthenticationType;
        }
    }
}
