using AutoMapper;
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
    public async Task<IEnumerable<Provider>> GetAll()
    {
        var providers = _mapper.Map<IEnumerable<Provider>>(await _providerRepository.GetAll());
        return providers;
    }
}
