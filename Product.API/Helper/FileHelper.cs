namespace Product.API.Helper;

public class FileHelper : IFileHelper
{
    private readonly IWebHostEnvironment _hostEnvironment;
    public FileHelper(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    public async Task<string> UploadFile(string filePath, IFormFile file)
    {
        try
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            
            string name = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf('.')).ToString();
            using var stream = new FileStream(filePath + name, FileMode.Create);
            await file.CopyToAsync(stream);
            return name;
        }
        catch
        {
            return null;
        }
    }
}