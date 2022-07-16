using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mineshop.Server.Model.Models.Payment;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Application.Controllers;

[Route("api/payment")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly IPaymentService _service;

    public PaymentController(IMapper mapper, IPaymentService service)
    {
        _mapper = mapper;
        _service = service;
    }

    /// <summary>
    ///     Procurar um pagamento pelo seu identificador
    /// </summary>
    [HttpGet("{identifier:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid identifier)
    {
        var viewModel = await _service.GetByIdentifier(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }

    /// <summary>
    ///     Procurar por pagamentos usando um filtro
    /// </summary>
    [HttpGet("")]
    public async Task<IActionResult> SearchAll([FromQuery] SearchPaymentViewModel search)
    {
        return Ok(await _service.SearchAll(search));
    }

    /// <summary>
    ///     Criar um novo pagamento
    /// </summary>
    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody] PostPaymentRequestViewModel request)
    {
        var viewModel = _mapper.Map<PaymentViewModel>(request);
        return CreatedAtAction(nameof(Post), await _service.CreateWithGateway(viewModel));
    }

    /// <summary>
    ///     Deletar um pagamento pelo seu identificador
    /// </summary>
    [HttpDelete("{identifier:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid identifier)
    {
        var viewModel = await _service.Delete(identifier);
        return viewModel != null ? Ok(viewModel) : NotFound();
    }
}