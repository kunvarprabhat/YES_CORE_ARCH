using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class Exam : AuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string ExamName { get; set; }
        public string Modules { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
    }
}
