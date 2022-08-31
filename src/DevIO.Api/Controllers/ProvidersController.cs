using AutoMapper;
using DevIO.Api.DTOs;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers;

[Route("api/fornecedores")]
public class ProvidersController : MainController
{
    private readonly IProviderRepository _providerRepository;
    private readonly IMapper _mapper;

    public ProvidersController(IProviderRepository providerRepository, IMapper mapper)
    {
        _providerRepository = providerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProviderDTO>> GetAll()
    {
        var providers = _mapper.Map<IEnumerable<ProviderDTO>>(await _providerRepository.GetAll());
        return providers;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProviderDTO>> GetById(Guid id)
    {
        var provider = await GetProviderProductsAddress(id);

        if (provider == null) return NotFound();

        return Ok(provider);
    }

    [HttpPost]
    public async Task<ActionResult<ProviderDTO>> Add(ProviderDTO providerDTO)
    {
        if (ModelState.IsValid) return BadRequest();

        var provider = _mapper.Map<Provider>(providerDTO);

        return Ok();
    }

    public async Task<ProviderDTO> GetProviderProductsAddress(Guid id)
    {
        return _mapper.Map<ProviderDTO>(await _providerRepository.GetProviderProductAddress(id));
    }
}
