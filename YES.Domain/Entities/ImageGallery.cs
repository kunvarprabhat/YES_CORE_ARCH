using System.ComponentModel.DataAnnotations;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class ImageGallery : BaseAuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }
        public string? ImageURL { get; set; }
        public string ImageType { get; set; }
        public bool IsActive { get; set; }
    }
}