using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Total)
               .HasColumnType("decimal(18,2)");

        // Configurar la navegación Items usando el backing field privado _items
        builder.HasMany(p => p.Items)
               .WithOne(i => i.Pedido)
               .HasForeignKey(i => i.PedidoId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indicar a EF Core que acceda al backing field directamente
        builder.Navigation(p => p.Items)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.ToTable("PedidoItems");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.PrecioUnitario)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(i => i.Prenda)
               .WithMany()
               .HasForeignKey(i => i.PrendaId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
