using System.ComponentModel.DataAnnotations;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class Registration : BaseAuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string RegistrationNo { get; set; }
        public string Name { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string Dob { get; set; }
        public string? IdentityType { get; set; }
        public string? IdentityNo { get; set; }
        public string Gender { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? SecurityQue { get; set; }
        public string? SecurityAnswer { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
