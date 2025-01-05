using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YES.Dtos.Common;
using YES.Dtos.Course;

namespace YES.Dtos.Syllabus
{
    public class SyllabusDto:BaseDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} select course name")]
        [DisplayName("Select Course")]
        public int CourseId { get; set; }
        public CourseDto? course { get; set; }
        public IEnumerable<CourseDto>? Courses { get; set; }
    }
}
