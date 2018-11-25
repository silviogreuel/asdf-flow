using System;
using System.Security.Claims;

namespace Asdf.Application.Api.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            try
            {
                return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (Exception e)
            {
                return GetUserName(principal);
            }
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
