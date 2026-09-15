using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EAMS.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "An unhandled exception occurred.");

        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        switch (exception)
        {
            case ValidationException validationException:

                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Validation failed.";

                problemDetails.Extensions["errors"] =
                    validationException.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            group => group.Key,
                            group => group
                                .Select(x => x.ErrorMessage)
                                .ToArray());

                break;

            case KeyNotFoundException:

                problemDetails.Status =
                    StatusCodes.Status404NotFound;

                problemDetails.Title = "Resource not found.";
                problemDetails.Detail = exception.Message;

                break;

            case UnauthorizedAccessException:

                problemDetails.Status =
                    StatusCodes.Status401Unauthorized;

                problemDetails.Title = "Unauthorized.";
                problemDetails.Detail = exception.Message;

                break;

            case InvalidOperationException:

                problemDetails.Status =
                    StatusCodes.Status400BadRequest;

                problemDetails.Title = "Invalid operation.";
                problemDetails.Detail = exception.Message;

                break;

            default:

                problemDetails.Status =
                    StatusCodes.Status500InternalServerError;

                problemDetails.Title =
                    "An unexpected error occurred.";

                problemDetails.Detail =
                    "An unexpected error occurred while processing the request.";

                break;
        }

        httpContext.Response.StatusCode =
            problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}