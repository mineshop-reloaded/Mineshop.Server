using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshop.Server.Model.Models.Server;
using Mineshop.Server.Service.Services.Interfaces.Server;

namespace Mineshop.Server.Application.Controllers.Server;

[Route("api/server")]
[ApiController]
public class ServerController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IServerService _service;

    public ServerController(IMapper mapper, IServerService service)
    {
        _mapper = mapper;
        _service = service;
    }

    /// <summary>
    ///     Encontra um servidor pelo seu identificador
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns>Servidor encontrado</returns>
    [Route("{identifier:guid}")]
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] Guid identifier)
    {
        var viewModel = await _service.GetByIdentifier(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }

    /// <summary>
    ///     Procurar por servidores
    /// </summary>
    /// <returns>Lista de servidores</returns>
    [HttpGet]
    public async Task<IActionResult> SearchAll([FromQuery] SearchServerViewModel search)
    {
        return Ok(await _service.SearchAll(search));
    }

    /// <summary>
    ///     Cria um novo servidor
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Servidor criado</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostServerRequestViewModel request)
    {
        var viewModel = _mapper.Map<ServerViewModel>(request);
        return CreatedAtAction(nameof(Post), await _service.Create(viewModel));
    }
}