using System.ComponentModel.DataAnnotations;

namespace YES.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
