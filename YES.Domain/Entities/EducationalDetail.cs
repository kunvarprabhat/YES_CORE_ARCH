using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YES.Domain.Entities
{
    public class EducationalDetail
    {
        [Key]
        public int Id { get; set; }
        public string ExamName { get; set; }
        public string BoardName { get; set; }
        public int YearOfPassing { get; set; }
        public int ObtainedMarks { get; set; }
        public int Percentage { get; set; }
        public bool IsSelected { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Students { get; set; }
    }
}
