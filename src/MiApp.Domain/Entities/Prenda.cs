using MiApp.Domain.Exceptions;

namespace MiApp.Domain.Entities;

public class Prenda
{
    // El ID principal de la base de datos
    public int Id { get; set; }

    // Datos principales
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    // Especificaciones
    public string Talle { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;

    // Datos comerciales
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public void Reservar(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad a reservar debe ser mayor a cero.");

        if (cantidad > Stock)
            throw new InsufficientStockException($"No hay stock suficiente para la prenda '{Nombre}'. Stock actual: {Stock}, solicitado: {cantidad}.");

        Stock -= cantidad;
    }
}