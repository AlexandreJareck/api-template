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
    private readonly IProviderService _providerService;
    private readonly IMapper _mapper;

    public ProvidersController(IProviderRepository providerRepository, IMapper mapper, IProviderService providerService)
    {
        _providerRepository = providerRepository;
        _mapper = mapper;
        _providerService = providerService;
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
        var provider = _mapper.Map<ProviderDTO>(await _providerRepository.GetProviderProductAddress(id));

        if (provider == null) return NotFound();

        return Ok(provider);
    }

    [HttpPost]
    public async Task<ActionResult<ProviderDTO>> Add(ProviderDTO providerDTO)
    {
        if (!ModelState.IsValid) return BadRequest();

        var provider = _mapper.Map<Provider>(providerDTO);

        await _providerService.Add(provider);

        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProviderDTO>> Update(Guid id, ProviderDTO providerDTO)
    {
        if (id != providerDTO.Id) return BadRequest();

        if (!ModelState.IsValid) return BadRequest();

        var provider = _mapper.Map<Provider>(providerDTO);

        await _providerService.Update(provider);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProviderDTO>> Delete(Guid id)
    {
        var provider = _mapper.Map<ProviderDTO>(await _providerRepository.GetProviderAddress(id));

        if (provider == null) return NotFound();

        await _providerService.Remove(id);

        return Ok();
    }
}
