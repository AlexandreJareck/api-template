using DevIO.Business.Interfaces;
using DevIO.Business.Models;

namespace DevIO.Business.Services;

public class ProviderService : BaseService, IProviderService
{
    private readonly IProviderRepository _providerRepository;
    private readonly IAddressRepository _addressRepository;

    public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository,
        INotifier notifier) : base(notifier)
    {
        _providerRepository = providerRepository;
        _addressRepository = addressRepository;
    }

    public async Task Add(Provider provider)
    {
        if (!ExecuteValidation(new ProviderValidation(), provider)
            || !ExecuteValidation(new AddressValidation(), provider?.Address!))
            return;

        if (_providerRepository.Get(p => p.Document == p.Document).Result.Any())
        {
            Notify("Já existe um fornecedor com este documento informado");
            return;
        }

        await _providerRepository.Add(provider!);
    }

    public async Task Remove(Guid id)
    {
        if (_providerRepository.GetProviderProductAddress(id).Result.Products!.Any())
        {
            Notify("O fornecedor possui produtos cadastrados!");
            return;
        }

        var address = await _addressRepository.GetAddressByProvider(id);

        if (address != null)
            await _addressRepository.Remove(address.Id);

        await _providerRepository.Remove(id);
    }

    public async Task Update(Provider provider)
    {
        if (!ExecuteValidation(new ProviderValidation(), provider)) return;

        if (_providerRepository.Get(p => p.Document == provider.Document && p.Id != provider.Id).Result.Any())
        {
            Notify("Já existe um fornecedor com este documento informado");
            return;
        }

        await _providerRepository.Update(provider);
    }

    public async Task UpdateAddress(Address address)
    {
        if (!ExecuteValidation(new AddressValidation(), address)) return;

        await _addressRepository.Update(address);
    }

    public void Dispose()
    {
        _providerRepository?.Dispose();
        _addressRepository?.Dispose();
    }
}
