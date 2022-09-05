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
    private readonly IAddressRepository _addressRepository;
    private readonly IProviderService _providerService;
    private readonly IMapper _mapper;

    public ProvidersController(IProviderRepository providerRepository,
                               IAddressRepository addressRepository,
                               IMapper mapper,
                               IProviderService providerService,
                               INotifier notifier) : base(notifier)
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
        var providerDTO = _mapper.Map<ProviderDTO>(await _providerRepository.GetProviderProductAddress(id));

        if (providerDTO == null) return NotFound();

        return Ok(providerDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ProviderDTO>> Add(ProviderDTO providerDTO)
    {
        if (!ModelState.IsValid) 
            return CustomResponse(ModelState);

        await _providerService.Add(_mapper.Map<Provider>(providerDTO));

        return CustomResponse(providerDTO);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProviderDTO>> Update(Guid id, ProviderDTO providerDTO)
    {
        if (id != providerDTO.Id)
        {
            NotifyError("Id inválido!");
            return CustomResponse(providerDTO);
        }

        if (!ModelState.IsValid) 
            return CustomResponse(ModelState);

        await _providerService.Update(_mapper.Map<Provider>(providerDTO));

        return CustomResponse(providerDTO);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProviderDTO>> Delete(Guid id)
    {
        var providerDTO = _mapper.Map<ProviderDTO>(await _providerRepository.GetProviderAddress(id));

        if (providerDTO == null) 
            return NotFound();

        await _providerService.Remove(id);

        return CustomResponse();
    }

    [HttpGet("obter-endereco/{id:guid}")]
    public async Task<AddressDTO> GetAddressById(Guid id)
    {
        var addressDTO = _mapper.Map<AddressDTO>(await _addressRepository.GetById(id));

        return addressDTO;
    }

    [HttpPut("atualizar-endereco/{id:guid}")]
    public async Task<IActionResult> UpdateAddress(Guid id, AddressDTO addressDTO)
    {
        if (id != addressDTO.Id)
        {
            NotifyError("Id inválido!");
            return CustomResponse(addressDTO);
        }

        if (!ModelState.IsValid) 
            return CustomResponse(ModelState);

        await _providerService.UpdateAddress(_mapper.Map<Address>(addressDTO));

        return CustomResponse(addressDTO);
    }
}
