using JWT.api.Models;
using JWT.api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JWT.api.Features.Authentication
{
    [Route("[controller]")]
    public class AuthenticationController : Controller

    {
        [HttpPost, AllowAnonymous]
        public IActionResult GetToken([FromBody] AuthenticationViewModel authentication)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid credentials.");
            if(authentication.User != "joe" || authentication.Password != "blow")
                return NotFound("Invalid credentials.");

            var user = new User() { Id = 1, Role = "Administrator" };

            return Ok(new
            {
                token_type = "bearer",
                access_token = TokenServices.CreateToken(ClaimTypes.CreateClaims(user)),
                expiration_date = DateTime.UtcNow.AddMinutes(TokenServices._expirationInMinute),

            });
        }

        [HttpDelete, TypeFilter(typeof(AuthenticationFilterAttribute))]
        public IActionResult SecuredDelete()
        {
            var user = HttpContext.User.GetUser();
            return Ok(new
            {
                Role = user.Role,
                AccessToDelete = true

            });
        }
    }
}
