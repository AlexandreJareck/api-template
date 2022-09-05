using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository;

public class ProviderRepository : Repository<Provider>, IProviderRepository 
{
    public ProviderRepository(MyDbContext context) : base(context)
    {

    }

    public async Task<Provider> GetProviderAddress(Guid id)
    {
        return await Db.Providers
            .AsNoTracking()
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.Id == id) 
            ?? new Provider();
    }

    public async Task<Provider> GetProviderProductAddress(Guid id)
    {
        return await Db.Providers
            .AsNoTracking()
            .Include(c => c.Products)
            .Include(c => c.Address)
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? new Provider();
    }
}
