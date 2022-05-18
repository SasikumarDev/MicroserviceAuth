using MongoDB.Driver;

namespace Product.API.Data;

public interface IMongoContext
{
    IMongoCollection<Models.Product> products { get; }
}