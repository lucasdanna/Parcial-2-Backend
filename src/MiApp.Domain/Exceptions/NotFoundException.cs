namespace MiApp.Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string entityName, object id) 
        : base($"{entityName} con ID {id} no fue encontrado.")
    {
    }
}
