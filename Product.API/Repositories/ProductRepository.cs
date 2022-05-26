using MongoDB.Driver;
using Product.API.Data;

namespace Product.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;
    public ProductRepository(IMongoContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
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

    public async Task<object> getProductsDisplay(string urlPath)
    {
        var data = await _context.products.Find(x => true).ToListAsync();
        var result = (from x in data
                      select new
                      {
                          x.Id,
                          x.Name,
                          x.Description,
                          x.Price,
                          x.Category,
                          Images = x.Images.Select(i => urlPath + i).ToList()
                      }).ToList();
        return result;
    }
}