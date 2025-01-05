using System.ComponentModel.DataAnnotations;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class News : BaseAuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
