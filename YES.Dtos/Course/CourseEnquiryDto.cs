using System.ComponentModel.DataAnnotations;
using YES.Dtos.Batches;
using YES.Dtos.Common;
using YES.Dtos.Syllabus;

namespace YES.Dtos.Course
{
    public class CourseEnquiryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]        
        public int CourseId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public BatchDto? BatchDto { get; set; }
        public List<SyllabusDto>? SyllabusDtos { get; set; }
    }
}
