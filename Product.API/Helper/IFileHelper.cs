namespace Product.API.Helper;

public interface IFileHelper
{
   Task<string> UploadFile(string filePath,IFormFile file);
}