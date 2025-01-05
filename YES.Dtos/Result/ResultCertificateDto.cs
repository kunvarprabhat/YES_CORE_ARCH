using YES.Dtos.Common;

namespace YES.Dtos.Result
{
    public class ResultCertificateDto : BaseDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Percentage { get; set; }
        public string Grad { get; set; }
        public string? Performance { get; set; }
        public string? MarksheetPath { get; set; }
        public string? MarksheetData { get; set; }
        public string? CertificatePath { get; set; }
        public string? CertificateData { get; set; }
        public DateTime MarksheetIssueDate { get; set; }
        public DateTime CertificateIssueDate { get; set; }
    }
}
