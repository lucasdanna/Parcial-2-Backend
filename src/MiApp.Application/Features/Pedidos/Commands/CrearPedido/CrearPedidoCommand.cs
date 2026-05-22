using MediatR;

namespace MiApp.Application.Features.Pedidos.Commands.CrearPedido;

public class CrearPedidoCommand : IRequest<int>
{
    public int ClienteId { get; set; }
    public List<CrearPedidoItemDto> Items { get; set; } = new();
}

public class CrearPedidoItemDto
{
    public int PrendaId { get; set; }
    public int Cantidad { get; set; }
}
