using JWT.api.Models;
using System.Linq;
using System.Security.Claims;

namespace JWT.api.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static User GetUser(this ClaimsPrincipal principal)
        {
            if (!principal.Claims.Any())
                return null;
  

            return new User()
            {
                Role = principal.Claims.First(c => c.Type == ClaimTypes.Role).Value
            };
        }
    }
}
