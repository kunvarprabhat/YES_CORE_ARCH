using System.ComponentModel.DataAnnotations;
using YES.Dtos.Course;
using YES.Dtos.Result;

namespace YES.Dtos.Student
{
    public class StudentProfileDto
    {
        public int Id { get; set; }
        public string? StudentId { get; set; }
        [Required]
        public int Category { get; set; }
        public string? Nationality { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfilePath { get; set; }
        public string? SignatureImage { get; set; }
        public string? SignaturePath { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int CId { get; set; }
        public CourseDto Course { get; set; }
        public RegistrationDto RegistrationDto { get; set; }
        public List<EducationalDetailDto> EducationalDetailDtos { get; set; }
        public List<ExamResultDto> ExamResultDtos { get; set; }
    }
}
