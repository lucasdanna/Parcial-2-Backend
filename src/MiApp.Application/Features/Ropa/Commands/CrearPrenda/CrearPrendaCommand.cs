using MediatR;

namespace MiApp.Application.Features.Ropa.Commands.CrearPrenda;

public class CrearPrendaCommand : IRequest<CrearPrendaResponse>
{
    // Datos principales
    public string Nombre { get; set; } = string.Empty; // Ej: "Remera Oversize 1892 CLUB"
    public string Descripcion { get; set; } = string.Empty;
    
    // Especificaciones de la ropa
    public string Talle { get; set; } = string.Empty; // Ej: "S", "M", "L", "XL"
    public string Color { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; // Ej: "Remeras", "Pantalones", "Buzos"
    
    // Datos comerciales
    public decimal Precio { get; set; }
    public int StockInicial { get; set; }
}