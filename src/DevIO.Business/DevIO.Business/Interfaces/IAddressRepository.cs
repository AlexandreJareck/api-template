using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IAddressRepository
{
    Task<Address> GetAddressByProvider(Guid providerId);
}
