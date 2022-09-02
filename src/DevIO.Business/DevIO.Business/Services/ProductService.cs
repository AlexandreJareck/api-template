using DevIO.Business.Interfaces;
using DevIO.Business.Models;

namespace DevIO.Business.Services;

public class ProductService : BaseService, IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Add(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        await _productRepository.Add(product);
    }

    public async Task Remove(Guid id)
    {
        await _productRepository.Remove(id);
    }

    public async Task Update(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        await _productRepository.Update(product);
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
    }
}
