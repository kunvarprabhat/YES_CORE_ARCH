using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YES.Domain.Auth;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class Branch : AuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string CenterName { get; set; }
        public string CenterId { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }
        public string? Profile { get; set; }
        public string? Signature { get; set; }
        public string? Qualification { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsVerify { get; set; } = false;
        public int BranchType { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual Student? Student { get; set; }
    }
}

