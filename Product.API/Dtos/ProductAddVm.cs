using System.ComponentModel.DataAnnotations;
using Product.API.Models;
using Product.API.Validators;

namespace Product.API.Dtos;

public class ProductAddVm
{
    [Required(ErrorMessage = "Product Name is Required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Product Description is Required")]
    public string Description { get; set; }

    [FileValidation(new string[] { ".jpg", ".jpeg", ".png", ".glb" }, true, null, true)]
    public List<IFormFile> Images { get; set; }

    [Required(ErrorMessage = "Product Price is Required")]
    public decimal Price { get; set; }
}