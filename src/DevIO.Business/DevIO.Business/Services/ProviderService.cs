using DevIO.Business.Interfaces;
using DevIO.Business.Models;

namespace DevIO.Business.Services;

public class ProviderService : BaseService, IProviderService
{
    public async Task Add(Provider provider)
    {
        if (!ExecuteValidation(new ProviderValidation(), provider))
            return;

        return;
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Provider provider)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAddress(Address address)
    {
        throw new NotImplementedException();
    }
}
