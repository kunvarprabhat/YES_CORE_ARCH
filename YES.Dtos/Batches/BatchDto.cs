using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using YES.Dtos.Course;
using YES.Dtos.Common;

namespace YES.Dtos.Batches
{
    public class BatchDto: BaseDto
    {
        public int Id { get; set; }
        public DateTime? UpcomingBatch { get; set; }
        public int? Size { get; set; }
        [Required(ErrorMessage = "{0} select course name")]
        [DisplayName("Select Course")]
        public int CourseId { get; set; }
        public CourseDto? courseDto { get; set; }
        public IEnumerable<CourseDto>? Courses { get; set; }
    }
}
