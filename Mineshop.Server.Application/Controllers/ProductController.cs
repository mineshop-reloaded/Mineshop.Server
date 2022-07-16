using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshop.Server.Model.Models.Product;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Application.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IProductService _service;

    public ProductController(IMapper mapper, IProductService service)
    {
        _mapper = mapper;
        _service = service;
    }

    /// <summary>
    ///     Procurar um product pelo seu identificador
    /// </summary>
    [HttpGet("{identifier:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid identifier)
    {
        var viewModel = await _service.GetByIdentifier(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }

    /// <summary>
    ///     Procurar por produtos usando um filtro
    /// </summary>
    [HttpGet("")]
    public async Task<IActionResult> SearchAll([FromQuery] SearchProductViewModel search)
    {
        return Ok(await _service.SearchAll(search));
    }

    /// <summary>
    ///     Criar um novo produto
    /// </summary>
    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody] PostProductRequestViewModel request)
    {
        var viewModel = _mapper.Map<ProductViewModel>(request);
        return CreatedAtAction(nameof(Post), await _service.Create(viewModel));
    }

    /// <summary>
    ///     Deletar um produto pelo seu identificador
    /// </summary>
    [HttpDelete("{identifier:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid identifier)
    {
        var viewModel = await _service.Delete(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }
}