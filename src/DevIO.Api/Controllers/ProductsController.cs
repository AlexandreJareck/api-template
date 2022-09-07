using AutoMapper;
using DevIO.Api.DTOs;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers;

[Route("api/produtos")]
public class ProductsController : MainController
{

    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(INotifier notifier,
                              IProductRepository productRepository,
                              IProductService productService,
                              IMapper mapper) : base(notifier)
    {
        _productRepository = productRepository;
        _productService = productService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Add(ProductDTO productDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var imgName = $"{Guid.NewGuid()}_{productDTO.Image}";

        if(!UploadFile(productDTO.ImageUpload, imgName))
            return CustomResponse();

        productDTO.Image = imgName;
        await _productService.Add(_mapper.Map<Product>(productDTO));

        return CustomResponse(productDTO);
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDTO>> GetAll()
    {
        return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetProductProviders());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDTO>> GetById(Guid id)
    {
        var productDTO = _mapper.Map<ProductDTO>(await _productRepository.GetProductProvider(id));

        if (productDTO == null) return NotFound();

        return CustomResponse(productDTO);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductDTO productDTO)
    {
        if (id != productDTO.Id)
        {
            NotifyError("Id inválido!");
            return CustomResponse();
        }

        var productUpdate = _mapper.Map<ProductDTO>(await _productRepository.GetProductProvider(id));

        if (string.IsNullOrEmpty(productDTO.Image))
            productDTO.Image = productUpdate.Image;

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (productDTO.ImageUpload != null)
        {
            var imagemNome = $"{Guid.NewGuid()}_{productDTO.Image}";

            if (!UploadFile(productDTO.ImageUpload, imagemNome))
                return CustomResponse(ModelState);

            productUpdate.Image = imagemNome;
        }

        productUpdate.ProviderId = productDTO.ProviderId;
        productUpdate.Name = productDTO.Name;
        productUpdate.Description = productDTO.Description;
        productUpdate.Value = productDTO.Value;
        productUpdate.Active = productDTO.Active;

        await _productService.Update(_mapper.Map<Product>(productUpdate));

        return CustomResponse(productDTO);
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProductDTO>> Delete(Guid id)
    {
        var product = _mapper.Map<ProductDTO>(await _productRepository.GetProductProvider(id));

        if (product == null) return NotFound();

        await _productService.Remove(id);

        return CustomResponse(product);
    }


    private bool UploadFile(string file, string imgName)
    {
        var imageDataByteArray = Convert.FromBase64String(file);

        if (file == null || file.Length <= 0)
        {
            NotifyError("Forneça uma imagem para este produto!");
            return false;
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgName);

        if (System.IO.File.Exists(filePath))
        {
            NotifyError("Já existe um arquivo com esse nome!");
            return false;
        }

        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }
}
