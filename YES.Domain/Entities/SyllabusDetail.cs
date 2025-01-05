using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class SyllabusDetail : AuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
