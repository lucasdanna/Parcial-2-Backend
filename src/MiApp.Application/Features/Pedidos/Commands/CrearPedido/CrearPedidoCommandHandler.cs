using MediatR;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;
using MiApp.Domain.Exceptions;

namespace MiApp.Application.Features.Pedidos.Commands.CrearPedido;

public class CrearPedidoCommandHandler : IRequestHandler<CrearPedidoCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CrearPedidoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CrearPedidoCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _context.Clientes.FindAsync(new object[] { request.ClienteId }, cancellationToken);
        if (cliente == null)
            throw new NotFoundException(nameof(Cliente), request.ClienteId);

        var pedido = new Pedido
        {
            ClienteId = request.ClienteId,
            Cliente = cliente
        };

        foreach (var itemDto in request.Items)
        {
            var prenda = await _context.Prendas.FindAsync(new object[] { itemDto.PrendaId }, cancellationToken);
            if (prenda == null)
                throw new NotFoundException(nameof(Prenda), itemDto.PrendaId);

            pedido.AgregarItem(prenda, itemDto.Cantidad);
        }

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync(cancellationToken);

        return pedido.Id;
    }
}
