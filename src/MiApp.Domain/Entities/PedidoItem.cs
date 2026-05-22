namespace MiApp.Domain.Entities;

public class PedidoItem
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }
    
    public int PrendaId { get; set; }
    public Prenda? Prenda { get; set; }
    
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
