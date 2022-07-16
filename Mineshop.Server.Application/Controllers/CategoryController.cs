using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshop.Server.Model.Models.Category;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Application.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly ICategoryService _service;

    public CategoryController(IMapper mapper, ICategoryService service)
    {
        _mapper = mapper;
        _service = service;
    }

    /// <summary>
    ///     Procurar uma categoria pelo seu identificador
    /// </summary>
    [HttpGet("{identifier:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid identifier)
    {
        var viewModel = await _service.GetByIdentifier(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }

    /// <summary>
    ///     Procurar por categorias usando um filtro
    /// </summary>
    [HttpGet("")]
    public async Task<IActionResult> SearchAll([FromQuery] SearchCategoryViewModel search)
    {
        return Ok(await _service.SearchAll(search));
    }

    /// <summary>
    ///     Criar uma nova categoria
    /// </summary>
    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody] PostCategoryRequestViewModel request)
    {
        var viewModel = _mapper.Map<CategoryViewModel>(request);
        return CreatedAtAction(nameof(Post), await _service.Create(viewModel));
    }

    /// <summary>
    ///     Deletar uma categoria pelo seu identificador
    /// </summary>
    [HttpDelete("{identifier:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid identifier)
    {
        var viewModel = await _service.Delete(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }
}