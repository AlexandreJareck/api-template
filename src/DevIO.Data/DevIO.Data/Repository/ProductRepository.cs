using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(MyDbContext context) : base(context) { }

    public async Task<Product> GetProductProvider(Guid id)
    {
        return await Db.Products
            .AsNoTracking()
            .Include(p => p.Provider)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? new Product();
    }

    public async Task<IEnumerable<Product>> GetProductProviders()
    {
        return await Db.Products
            .AsNoTracking()
            .Include(p => p.Provider).OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
    {
        return await Get(p => p.ProviderId == providerId);
    }
}
