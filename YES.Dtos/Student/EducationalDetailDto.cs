using System.ComponentModel.DataAnnotations;

namespace YES.Dtos.Student
{
    public class EducationalDetailDto
    {
        public int Id { get; set; }
        [Required]
        public string ExamName { get; set; }
        [Required]
        public string BoardName { get; set; }
        public int YearOfPassing { get; set; }
        public int ObtainedMarks { get; set; }
        public int TotalMarks { get; set; }
        public int Percentage { get; set; }
        public bool IsSelected { get; set; }
    }
}
