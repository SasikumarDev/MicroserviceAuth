namespace Product.API.Repositories;

public interface IUnitofWork
{
    IProductRepository productRepository { get; }
}