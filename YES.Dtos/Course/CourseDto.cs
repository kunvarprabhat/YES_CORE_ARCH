using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YES.Dtos.Course
{
    public class CourseDto
    {
        public int? CourseId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Course Sort Name")]
        public string CourseShortName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Course Full Name")]
        public string CourseFullName { get; set; }
        [Required]
        public string? Duration { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class CourseVm
    {
        public List<CourseDto> CourseDtos { get; set; }
    }
}
