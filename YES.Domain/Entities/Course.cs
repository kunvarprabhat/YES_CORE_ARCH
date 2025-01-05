using System.ComponentModel.DataAnnotations;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class Course : AuditEntity<int>
    {
        public Course()
        {
            SyllabusDetails = new HashSet<SyllabusDetail>();
        }
        [Key]
        public int Id { get; set; }
        public string CourseShortName { get; set; }
        public string CourseFullName { get; set; }
        public string? Detalis { get; set; }
        public string? Duration { get; set; }
        public BatchDetails BatchDetails { get; set; }
        public ICollection<SyllabusDetail> SyllabusDetails { get; set; }
    }
}
