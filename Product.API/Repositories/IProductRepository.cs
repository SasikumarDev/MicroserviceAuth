namespace Product.API.Repositories;

public interface IProductRepository
{
    Task CreateProduct(Models.Product product);
    Task<IEnumerable<Models.Product>> GetAll();
    Task<bool> DeleteProduct(string id);
}