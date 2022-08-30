using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId);
    Task<IEnumerable<Product>> GetProductProviders();
    Task<Product> GetProductProvider(Guid id);
}
