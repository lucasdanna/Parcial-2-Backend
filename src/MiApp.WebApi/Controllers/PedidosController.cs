using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Features.Pedidos.Commands.CrearPedido;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PedidosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crea un nuevo pedido.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> Crear([FromBody] CrearPedidoCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Crear), new { id = id }, id);
    }
}
