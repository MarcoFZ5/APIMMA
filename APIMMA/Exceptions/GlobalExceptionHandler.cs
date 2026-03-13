using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APIMMA.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occurred while processing the request.");

            ProblemDetails problemDetails;

            var status = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ConflictException => StatusCodes.Status409Conflict,
                FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
                DbUpdateException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            if (exception is FluentValidation.ValidationException validationException)
            {
                problemDetails = new ProblemDetails
                {
                    Title = exception.Message.Substring(0, 17),
                    Instance = httpContext.Request.Path,
                    Extensions =
                    {
                        ["Errors"] = validationException.Errors.
                        GroupBy(error => error.PropertyName)
                        .ToDictionary(group => group.Key, group => group
                        .Select(error => error.ErrorMessage))
                    }
                };
            }
            else
            {
                problemDetails = new ProblemDetails
                {
                    Title = exception.Message,
                    Instance = httpContext.Request.Path
                };
            }

            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return await ValueTask.FromResult(true);
        }
    }
}
