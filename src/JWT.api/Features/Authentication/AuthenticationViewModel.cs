using System.ComponentModel.DataAnnotations;

namespace JWT.api.Features.Authentication
{
    public class AuthenticationViewModel
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
