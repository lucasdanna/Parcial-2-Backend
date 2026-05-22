using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MiApp.Domain.Exceptions;
using FluentValidation;

namespace MiApp.WebApi.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Title = "Errores de validación";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = "Se encontraron uno o más errores de validación.";
            
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(g => g.Key, g => g.ToArray());
                
            problemDetails.Extensions.Add("errors", errors);
        }
        else if (exception is NotFoundException notFoundException)
        {
            problemDetails.Title = "Recurso no encontrado";
            problemDetails.Status = StatusCodes.Status404NotFound;
            problemDetails.Detail = notFoundException.Message;
        }
        else if (exception is InsufficientStockException insufficientStockException)
        {
            problemDetails.Title = "Stock insuficiente";
            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
            problemDetails.Detail = insufficientStockException.Message;
        }
        else if (exception is DomainException domainException)
        {
            problemDetails.Title = "Error de dominio";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = domainException.Message;
        }
        else
        {
            problemDetails.Title = "Error interno del servidor";
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Detail = "Ha ocurrido un error inesperado.";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
