
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
#nullable disable
    public class ExamResult : BaseAuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Tearm { get; set; }
        public int ObtainMark { get; set; }
        public int TotalMarks { get; set; }
        public int Percentage { get; set; }
        public string Grad { get; set; }
        public int ExamId { get; set; }
        [ForeignKey("ExamId")]
        public virtual Exam Exams { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
