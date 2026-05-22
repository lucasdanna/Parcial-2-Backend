using MediatR;
using Microsoft.EntityFrameworkCore;
using MiApp.Application.Contracts.Persistence;

namespace MiApp.Application.Features.Ropa.Queries.ObtenerTodasLasPrendas;

public class ObtenerTodasLasPrendasQueryHandler : IRequestHandler<ObtenerTodasLasPrendasQuery, List<ObtenerTodasLasPrendasResponse>>
{
    private readonly IApplicationDbContext _context;

    public ObtenerTodasLasPrendasQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ObtenerTodasLasPrendasResponse>> Handle(ObtenerTodasLasPrendasQuery request, CancellationToken cancellationToken)
    {
        var prendas = await _context.Prendas
            .AsNoTracking()
            .Select(p => new ObtenerTodasLasPrendasResponse
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Precio = p.Precio,
                StockDisponible = p.Stock
            })
            .ToListAsync(cancellationToken);

        return prendas;
    }
}