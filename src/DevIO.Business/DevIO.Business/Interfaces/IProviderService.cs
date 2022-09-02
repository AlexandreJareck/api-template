using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IProviderService : IDisposable
{
    Task Add(Provider provider);
    Task Update(Provider provider);
    Task Remove(Guid id);

    Task UpdateAddress(Address address);
}
