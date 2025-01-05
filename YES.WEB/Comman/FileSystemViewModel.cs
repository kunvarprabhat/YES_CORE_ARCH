namespace YES.Comman;

public class FileSystemViewModel : FileUploadViewModelBase
{
    public int StudentId { get; set; }
    /// <summary>
    /// Save to Server File System 
    /// </summary>
    /// <returns>A Task with true=successful, false=unsuccessful</returns>
    protected override async Task<bool> SaveToDataStoreAsync()
    {
        bool ret = false;

        if (FileToUpload != null)
        {
            if (FileInfo.Type.StartsWith("image"))
            {
                // Create a random file name for this new file
                // Leave the file extension in place if it is an image file
                string ext = Path.GetExtension(FileInfo.FileName).ToLower();
                string fileName = PrefixName + GetDateTime();
                FileInfo.FileNameOnServer = Path.Combine(FileInfo.ServerLocation, fileName + ext);
                FileInfo.FileName = fileName + ext;
            }
            else
            {
                // Create a random file name for this new file
                FileInfo.FileNameOnServer = Path.Combine(FileInfo.ServerLocation, Path.GetRandomFileName());
            }

            if (!Directory.Exists(FileInfo.ServerLocation))
            {
                Directory.CreateDirectory(FileInfo.ServerLocation);
            }
            // Create a stream to write the file to
            using var stream = File.Create(FileInfo.FileNameOnServer);

            // Upload file and copy to the stream
            await FileToUpload.CopyToAsync(stream);

            ret = true;
        }

        return ret;
    }
    public long GetDateTime() => DateTimeOffset.Now.ToUnixTimeSeconds();
}
