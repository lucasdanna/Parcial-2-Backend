using FluentValidation;
using FluentValidation.Results;
using MiApp.Application.Features.Ropa.Commands.CrearPrenda;

namespace MiApp.Application.Features.Ropa.Commands.CrearPrenda;

public class CrearPrendaCommandValidator : AbstractValidator<CrearPrendaCommand>
{
    public CrearPrendaCommandValidator()
    {
        RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El nombre de la prenda es requerido.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(p => p.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

        RuleFor(p => p.StockInicial)
            .GreaterThanOrEqualTo(0).WithMessage("El stock inicial no puede ser negativo.");

        RuleFor(p => p.Categoria)
            .NotEmpty().WithMessage("La categoría es requerida.");
    }
}
