using Microsoft.AspNetCore.Http;

namespace YES.Dtos.Common
{
    public class BaseFileUpload
    {
        public IFormFile? FileToUpload { get; set; }
        public IFormFile? FileSignature { get; set; }
    }
}
