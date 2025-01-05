using System.ComponentModel.DataAnnotations;

namespace YES.Dtos.Account
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
