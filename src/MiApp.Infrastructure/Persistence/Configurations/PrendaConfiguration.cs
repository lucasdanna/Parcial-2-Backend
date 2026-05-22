using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence.Configurations;

public class PrendaConfiguration : IEntityTypeConfiguration<Prenda>
{
    public void Configure(EntityTypeBuilder<Prenda> builder)
    {
        // El nombre de la tabla
        builder.ToTable("Prendas");

        // Definimos la clave primaria (ID)
        builder.HasKey(p => p.Id);

        // Reglas para las columnas
        builder.Property(p => p.Nombre)
               .IsRequired()
               .HasMaxLength(150); // El nombre no puede quedar vacío ni pasar los 150 caracteres

        builder.Property(p => p.Precio)
               .HasColumnType("decimal(18,2)"); // Para que los precios tengan 2 decimales
    }
}