using System.ComponentModel.DataAnnotations;

namespace YES.Domain.EntityClasses;

public class FileInformation
{
    public FileInformation()
    {
        Title = string.Empty;
        Description = string.Empty;
        FileName = string.Empty;
        Type = string.Empty;
        ServerLocation = string.Empty;
        FileNameThumbnail = string.Empty;
        FileNameOnServer = string.Empty;
    }

    [Display(Name = "Enter Title for File")]
    public string Title { get; set; }
    [Display(Name = "Enter Description for File")]
    public string Description { get; set; }
    public string FileName { get; set; }
    public string FileNameThumbnail { get; set; }
    public string FileNameOnServer { get; set; }
    public long Length { get; set; }
    public double FileSize { get; set; }
    public string Type { get; set; }
    public string ServerLocation { get; set; }
}
