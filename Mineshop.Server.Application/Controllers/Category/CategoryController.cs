using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshop.Server.Model.Models.Category;
using Mineshop.Server.Service.Services.Interfaces.Category;

namespace Mineshop.Server.Application.Controllers.Category;

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
    ///     Encontra uma categoria pelo seu identificador
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns>Categoria encontrada</returns>
    [Route("{identifier:guid}")]
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] Guid identifier)
    {
        var viewModel = await _service.GetByIdentifier(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }

    /// <summary>
    ///     Procurar por categorias
    /// </summary>
    /// <returns>Lista de categorias</returns>
    [HttpGet]
    public async Task<IActionResult> SearchAll([FromQuery] SearchCategoryViewModel search)
    {
        return Ok(await _service.SearchAll(search));
    }

    /// <summary>
    ///     Cria uma nova categoria
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Categoria criada</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostCategoryRequestViewModel request)
    {
        var viewModel = _mapper.Map<CategoryViewModel>(request);
        return CreatedAtAction(nameof(Post), await _service.Create(viewModel));
    }
}