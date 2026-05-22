namespace MiApp.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }

    private readonly List<PedidoItem> _items = new();
    public IReadOnlyCollection<PedidoItem> Items => _items.AsReadOnly();

    public void AgregarItem(Prenda prenda, int cantidad)
    {
        var item = new PedidoItem
        {
            PrendaId = prenda.Id,
            Prenda = prenda,
            Cantidad = cantidad,
            PrecioUnitario = prenda.Precio
        };
        
        prenda.Reservar(cantidad);
        
        _items.Add(item);
        Total += item.PrecioUnitario * item.Cantidad;
    }
}
