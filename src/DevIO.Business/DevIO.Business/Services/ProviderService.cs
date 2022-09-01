using DevIO.Business.Interfaces;
using DevIO.Business.Models;

namespace DevIO.Business.Services;

public class ProviderService : BaseService, IProviderService
{
    public async Task Add(Provider provider)
    {
        if (!ExecuteValidation(new ProviderValidation(), provider) 
            && !ExecuteValidation(new AddressValidation(), provider.Addreess))
            return;

    }

    public async Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Provider provider)
    {
        if (!ExecuteValidation(new ProviderValidation(), provider)) return;
    }

    public async Task UpdateAddress(Address address)
    {
        if (!ExecuteValidation(new AddressValidation(), address)) return;
    }
}
