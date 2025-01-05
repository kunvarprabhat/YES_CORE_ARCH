using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class Student : BaseAuditEntity<int>
    {

        public Student()
        {
            EducationalDetails = new HashSet<EducationalDetail>();
            ExamResults = new HashSet<ExamResult>();
        }
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string Category { get; set; }
        public string? Nationality { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfilePath { get; set; }
        public string? SignatureImage { get; set; }
        public string? SignaturePath { get; set; }
        public int Status { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public int RegistrationId { get; set; }
        [ForeignKey("RegistrationId")]
        public virtual Registration Registration { get; set; }
        public virtual ResultCertificate? ResultCertificate { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<EducationalDetail>? EducationalDetails { get; set; }
        public virtual ICollection<ExamResult>? ExamResults { get; set; }
    }
}
