using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YES.Dtos.Branch;
using YES.Dtos.Common;
using YES.Dtos.Course;
using YES.Dtos.Result;

namespace YES.Dtos.Student
{
    public class StudentDto : BaseDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        [Required]
        public string Category { get; set; }
        public string? Nationality { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfilePath { get; set; }
        public string? SignatureImage { get; set; }
        public string? SignaturePath { get; set; }
        public int Status { get; set; } = 0;
        [Required(ErrorMessage = "select course")]
        public int CourseId { get; set; }
        public CourseDto? Course { get; set; }
        public RegistrationDto? RegistrationDto { get; set; }
        public ResultCertificateDto? ResultCertificateDto { get; set; }
        public ExamResultDto? ExamResultDto { get; set; }
        public BranchDto? BranchDto { get; set; }

        public List<EducationalDetailDto>? EducationalDetailDtos { get; set; }
        public List<ExamResultDto>? ExamResultDtos { get; set; }

    }
}
