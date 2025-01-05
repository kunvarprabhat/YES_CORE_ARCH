using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YES.Dtos.Course;

namespace YES.Dtos.ExamResult
{
    public class ExamDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Exam Name")]
        public string ExamName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Assesment/Course/Modules")]
        public string Modules { get; set; }
        [Required(ErrorMessage = "{0} select course")]
        [DisplayName("Course Name")]
        public int CourseId { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public CourseDto? course { get; set; }
    }
    public class ExamVm
    {
        public IEnumerable<CourseDto>? Courses { get; set; }
        public List<ExamDto> examDtos { get; set; }
    }
}