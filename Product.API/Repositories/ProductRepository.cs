using MongoDB.Driver;
using Product.API.Data;

namespace Product.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoContext _context;
    public ProductRepository(IMongoContext context)
    {
        _context = context;
    }
    public async Task CreateProduct(Models.Product product)
    {
        await _context.products.InsertOneAsync(product);
    }

    public Task<bool> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Models.Product>> GetAll()
    {
        return await _context.products.Find(x => true).ToListAsync();
    }
}