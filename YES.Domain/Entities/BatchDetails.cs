using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class BatchDetails : AuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public DateTime? UpcomingBatch { get; set; }
        public int? Size { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
    }
}
