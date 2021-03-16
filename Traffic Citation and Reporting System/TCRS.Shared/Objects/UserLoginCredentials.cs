using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TCRS.Shared.Objects
{
    public class UserLoginCredentials
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
