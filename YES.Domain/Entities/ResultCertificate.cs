using System.ComponentModel.DataAnnotations.Schema;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class ResultCertificate : AuditEntity<int>
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; }
        public string Grad { get; set; }
        public string? MarksheetPath { get; set; }
        public string? MarksheetData { get; set; }
        public string? CertificatePath { get; set; }
        public string? CertificateData { get; set; }
        public DateTime MarksheetIssueDate { get; set; }
        public DateTime CertificateIssueDate { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

    }
}
