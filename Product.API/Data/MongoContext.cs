using MongoDB.Driver;

namespace Product.API.Data;

public class MongoContext : IMongoContext
{
    public MongoContext(IConfiguration _configuration)
    {
        var client = new MongoClient(_configuration["ConnectionStrings:Server"].ToString());
        var db = client.GetDatabase(_configuration["ConnectionStrings:Db"].ToString());
        products = db.GetCollection<Models.Product>("Product");
    }

    public IMongoCollection<Models.Product> products { get; private set; }
}