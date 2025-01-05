using Microsoft.AspNetCore.Http;

namespace YES.Dtos.Student
{
#nullable disable
    public class StudentProfileImage
    {
        public int StudentId { get; set; }
        public string ProfileImageName { get; set; }
        public string ProfileFolderPath { get; set; }
        public string SignatureImageName { get; set; }
        public string SignatureFolderPath { get; set; }
    }
}
