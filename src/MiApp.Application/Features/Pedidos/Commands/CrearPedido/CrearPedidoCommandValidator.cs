using FluentValidation;

namespace MiApp.Application.Features.Pedidos.Commands.CrearPedido;

public class CrearPedidoCommandValidator : AbstractValidator<CrearPedidoCommand>
{
    public CrearPedidoCommandValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0).WithMessage("El ID del cliente es requerido y debe ser mayor a cero.");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("El pedido debe tener al menos un ítem.");

        RuleForEach(x => x.Items).SetValidator(new CrearPedidoItemDtoValidator());
    }
}

public class CrearPedidoItemDtoValidator : AbstractValidator<CrearPedidoItemDto>
{
    public CrearPedidoItemDtoValidator()
    {
        RuleFor(x => x.PrendaId)
            .GreaterThan(0).WithMessage("El ID de la prenda debe ser mayor a cero.");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");
    }
}
