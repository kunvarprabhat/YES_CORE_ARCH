using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YES.Dtos.Common;
using YES.Dtos.Student;

namespace YES.Dtos.Branch
{
#nullable disable
    public class BranchDto : BaseFileUpload
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name of manager")]
        public string Name { get; set; }
        [DisplayName("Last Name of manager")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        //[EmailUnique]  // Custom validation attribute
        [DisplayName("Email Id")]
        public string EmailId { get; set; }
        [Required]
        [DisplayName("Mobiler number")]
        public string PhoneNo { get; set; }
        [Required]
        [DisplayName("Center Name")]
        public string CenterName { get; set; }
        [Required]
        [DisplayName("Center Id")]
        public string CenterId { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [Required]
        public string? Address { get; set; }

        public string? Profile { get; set; }
        public string? Signature { get; set; }
        public string? Qualification { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerify { get; set; }
        public int BranchType { get; set; }
        public string Password { get; set; }
        public UserDto userDto { get; set; }
        public StudentDto? StudentDto { get; set; }

    }
}
