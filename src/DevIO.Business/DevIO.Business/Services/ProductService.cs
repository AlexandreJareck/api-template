using DevIO.Business.Interfaces;
using DevIO.Business.Models;

namespace DevIO.Business.Services;

public class ProductService : BaseService, IProductService
{
    public async Task Add(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;
    }

    public async Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;
    }
}
