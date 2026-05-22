using MediatR;
using MiApp.Application.Contracts.Persistence;

namespace MiApp.Application.Features.Ropa.Queries.ObtenerPrendaPorId;

public class ObtenerPrendaPorIdQueryHandler : IRequestHandler<ObtenerPrendaPorIdQuery, ObtenerPrendaPorIdResponse>
{
    private readonly IApplicationDbContext _context;

    public ObtenerPrendaPorIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ObtenerPrendaPorIdResponse> Handle(ObtenerPrendaPorIdQuery request, CancellationToken cancellationToken)
    {
        var prenda = await _context.Prendas.FindAsync(new object[] { request.Id }, cancellationToken);

        if (prenda == null)
            throw new MiApp.Domain.Exceptions.NotFoundException(nameof(MiApp.Domain.Entities.Prenda), request.Id);

        return new ObtenerPrendaPorIdResponse
        {
            Id = prenda.Id,
            Nombre = prenda.Nombre,
            Descripcion = prenda.Descripcion,
            Talle = prenda.Talle,
            Color = prenda.Color,
            Categoria = prenda.Categoria,
            Precio = prenda.Precio,
            StockDisponible = prenda.Stock
        };
    }
}