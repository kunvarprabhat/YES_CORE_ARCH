
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YES.Dtos.Common;

namespace YES.Dtos.Student
{
    public class RegistrationDto : BaseDto
    {
        public int Id { get; set; }
        public string RegistrationNo { get; set; }
        [Required(ErrorMessage = "Please enter your Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Father's Name")]
        public string? FatherName { get; set; }
        [Required]
        [DisplayName("Mother's Name")]
        public string? MotherName { get; set; }
        public string Dob { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public string Years { get; set; }
        [Required]
        public string? IdentityNo { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? PinCode { get; set; }
        [Required]
        [DisplayName("Mobiler Number")]
        public string MobileNo { get; set; }
        [Required]
        [DisplayName("Email Id")]
        public string EmailId { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? SecurityQuestion { get; set; }
        public string? SecurityAnswer { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
