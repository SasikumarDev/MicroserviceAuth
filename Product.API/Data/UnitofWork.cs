
using Product.API.Repositories;

namespace Product.API.Data;
public class UnitofWork : IUnitofWork, IDisposable
{
    public IProductRepository productRepository { get; private set; }
    public UnitofWork(IMongoContext _context)
    {
        productRepository = new ProductRepository(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}