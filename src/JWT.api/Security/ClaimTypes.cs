using JWT.api.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace JWT.api.Security
{
    public class ClaimTypes
    {
        public const string Id = "Id";
        public const string Role = "Role";

        public static IEnumerable<Claim> CreateClaims(User user)
        {
            return new Claim[]
            {
                new Claim(Id, user.Id.ToString()),
                new Claim(Role, user.Role)
            };
        }
    }
}
