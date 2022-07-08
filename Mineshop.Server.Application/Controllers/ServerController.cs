using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshop.Server.Model.Models.Server;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Application.Controllers;

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
    ///     Procurar um servidor pelo seu identificador
    /// </summary>
    [HttpGet("{identifier:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid identifier)
    {
        var viewModel = await _service.GetByIdentifier(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }

    /// <summary>
    ///     Procurar por servidores usando um filtro
    /// </summary>
    [HttpGet("")]
    public async Task<IActionResult> SearchAll([FromQuery] SearchServerViewModel search)
    {
        return Ok(await _service.SearchAll(search));
    }

    /// <summary>
    ///     Cria um novo servidor
    /// </summary>
    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody] PostServerRequestViewModel request)
    {
        var viewModel = _mapper.Map<ServerViewModel>(request);
        return CreatedAtAction(nameof(Post), await _service.Create(viewModel));
    }

    /// <summary>
    ///     Deleta um servidor pelo seu identificador
    /// </summary>
    [HttpDelete("{identifier:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid identifier)
    {
        var viewModel = await _service.Delete(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }
}