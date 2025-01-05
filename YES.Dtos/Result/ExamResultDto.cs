using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using YES.Dtos.ExamResult;

namespace YES.Dtos.Result
{
    public class ExamResultDto
    {
        public int? Id { get; set; }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Assesment/Course/Modules")]
        public string? Tearm { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Obtain Mark")]
        public decimal ObtainMark { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Total Mark")]
        public decimal TotalMarks { get; set; }
        public decimal Percentage { get; set; }
        public string? Grad { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "{0} select exam")]
        public int ExamId { get; set; }

        public ExamDto? ExamDto { get; set; }

    }
}
