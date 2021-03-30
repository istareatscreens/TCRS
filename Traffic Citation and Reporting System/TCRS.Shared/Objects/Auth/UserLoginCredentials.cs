using System.ComponentModel.DataAnnotations;

namespace TCRS.Shared.Objects.Auth
{
    public class UserLoginCredentials
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
