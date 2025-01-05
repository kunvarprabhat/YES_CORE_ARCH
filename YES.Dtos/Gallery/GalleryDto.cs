using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using YES.Dtos.Common;

namespace YES.Dtos.Gallery
{
#nullable disable
    public class GalleryDto : BaseDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string ImageType { get; set; }
        [Required]
        public IFormFile FileToUpload { get; set; }
    }
}
