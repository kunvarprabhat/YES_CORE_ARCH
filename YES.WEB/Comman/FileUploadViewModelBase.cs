using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.ComponentModel.DataAnnotations;
using YES.Domain.EntityClasses;

namespace YES.Comman;

public abstract class FileUploadViewModelBase
{
    public FileInformation FileInfo { get; set; } = new();
    [Display(Name = "Select File to Upload")]
    [Required(ErrorMessage = "Enter a File to Upload")]
    public IFormFile? FileToUpload { get; set; }
    public FileUploadSettings UploadSettings { get; set; } = new();
    public string ErrorMessage { get; set; } = string.Empty;
    public string PrefixName { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Width { get; set; }

    public virtual async Task<bool> Save()
    {
        bool ret = false;
        // Set Server Location to Store File
        FileInfo.ServerLocation = UploadSettings.UploadFolderPath + @"\";
        // Ensure user selected a file for upload
        if (FileToUpload != null && FileToUpload.Length > 0)
        {
            // Get all File Properties
            GetFileProperties();
            if (Validate())
            {

                FileInfo.FileSize = FileInfo.Length / 1024;
                if (FileInfo.Type.StartsWith("image"))
                {                    // Save File to Data Store
                    if (FileInfo.FileSize <= 6)
                    {
                        ret = await SaveToDataStoreAsync();
                    }
                    else
                    {
                        ret = await SaveToDataStoreAsync();
                        GenerateThumbnail();
                    }

                }
            }
        }

        return ret;
    }

    protected virtual void GetFileProperties()
    {
        if (FileToUpload != null)
        {
            // Get the file name
            FileInfo.FileName = FileToUpload.FileName;
            // Get the file's length
            FileInfo.Length = FileToUpload.Length;
            // Get the file's type
            FileInfo.Type = FileToUpload.ContentType;
        }
    }

    public virtual bool Validate()
    {
        bool ret = false;
        string[] validTypes = UploadSettings.ValidFileTypes.Split(",");
        string[] extensions = UploadSettings.InvalidFileExtensions.Split(",");

        // Check for valid "accept" types
        ret = validTypes.Any(f => FileInfo.Type.StartsWith(f));

        if (ret)
        {
            // Check for invalid file extensions
            ret = !extensions.Any(f => Path.GetExtension(FileInfo.FileName) == f);
        }

        if (!ret)
        {
            ErrorMessage = "You are Trying to Upload an Invalid File Type";
        }

        return ret;
    }

    // Override to save to a data store
    protected abstract Task<bool> SaveToDataStoreAsync();

    /// <summary>
    /// Generate a thumbnail for the uploaded image
    /// </summary>
    public bool GenerateThumbnail()
    {
        // Set FileNameThumbnail property
        FileInfo.FileNameThumbnail = PrefixName + GetDateTime() + "-Thumb" + Path.GetExtension(FileInfo.FileNameOnServer).ToLower();
        FileInfo.FileName = FileInfo.FileNameThumbnail;
        // Load the original image
        using Image image = Image.Load(FileInfo.FileNameOnServer);
        // Resize the image to a percentage of the size
        if (Height != 0 && Width != 0)
            image.Mutate(x => x.Resize(Width, Height));
        else
            image.Mutate(x => x
             .Resize(
               (int)(image.Width * UploadSettings.ThumbScale),
               (int)(image.Height * UploadSettings.ThumbScale)));

        // Save the thumbnail
        if (!Directory.Exists(FileInfo.ServerLocation))
        {
            Directory.CreateDirectory(FileInfo.ServerLocation);
        }
        image.Save(Path.Combine(FileInfo.ServerLocation, FileInfo.FileNameThumbnail));
        if (File.Exists(FileInfo.ServerLocation))
        {
            // If file found, delete it
            File.Delete(FileInfo.ServerLocation);
            Console.WriteLine("File deleted.");
        }
        return true;
    }
    public long GetDateTime() => DateTimeOffset.Now.ToUnixTimeSeconds();
}