using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Model.Models.Server;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Application.Controllers;

[Route("api/server")]
[ApiController]
public class ServerController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IServerService _serverService;

    public ServerController(IMapper mapper, IServerService serverService)
    {
        _mapper = mapper;
        _serverService = serverService;
    }

    /// <summary>
    /// Encontra um servidor pelo seu identificador
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns>Servidor encontrado</returns>
    [Route("{identifier:guid}")]
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] Guid identifier)
    {
        var viewModel = await _serverService.GetByIdentifier(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }

    /// <summary>
    /// Lista todos os servidores existentes
    /// </summary>
    /// <returns>Lista de servidores</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var viewModelList = await _serverService.GetAll();
        return Ok(viewModelList);
    }

    /// <summary>
    /// Cria um novo servidor
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/server
    ///     {
    ///        "name": "Server 1"
    ///     }
    /// </remarks>
    /// <param name="request"></param>
    /// <returns>Servidor criado</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostServerRequestViewModel request)
    {
        var viewModel = _mapper.Map<ServerViewModel>(request);
        return CreatedAtAction(nameof(Post), await _serverService.Create(viewModel));
    }
}