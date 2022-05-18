using System.ComponentModel.DataAnnotations;

namespace Product.API.Validators;

public class FileValidation : ValidationAttribute
{
    private readonly string[] _allowedExtensions;
    private readonly bool _allowMultipleFiles;
    private readonly string _errorMessage;
    private readonly bool _isRequired;
    public FileValidation(string[] allowedExtensions, bool allowMultipleFiles, string errorMessage, bool Required)
    {
        _allowedExtensions = allowedExtensions;
        _allowMultipleFiles = allowMultipleFiles;
        _errorMessage = errorMessage;
        _isRequired = Required;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            if (value is null && _isRequired)
            {
                return new ValidationResult("Atleast one file is required");
            }
            else if (value is null && !_isRequired)
            {
                return ValidationResult.Success;
            }

            if (_allowMultipleFiles)
            {
                foreach (var file in value as List<IFormFile>)
                {
                    string exe = Path.GetExtension(file.FileName);
                    if (!_allowedExtensions.Any(x => x.ToLower() == exe.ToLower()))
                    {
                        return new ValidationResult(_errorMessage ?? $"Allowed file formats : {string.Join(',', _allowedExtensions)}");
                    }
                }
                return ValidationResult.Success;
            }
            else
            {
                var file = value as IFormFile;
                string exe = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Any(x => x.ToLower() == exe.ToLower()))
                {
                    return new ValidationResult(_errorMessage ?? $"Allowed file formats : {string.Join(',', _allowedExtensions)}");
                }
                return ValidationResult.Success;
            }
        }
        catch (Exception ex)
        {
            return new ValidationResult($"{ex.Message}");
        }
    }
}