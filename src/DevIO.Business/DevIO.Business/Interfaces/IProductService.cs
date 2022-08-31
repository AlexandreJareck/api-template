using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IProductService
{
    Task Add(Product product);
    Task Update(Product product);
    Task Remove(Guid id);
}
