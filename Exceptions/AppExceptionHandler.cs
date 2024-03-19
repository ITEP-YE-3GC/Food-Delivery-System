using Microsoft.AspNetCore.Diagnostics;

namespace OrderService.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> _logger;

        public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                ForbidException => (403, "Forbidden"),
                BadHttpRequestException badHttpRequestException => (400, badHttpRequestException.Message),
                NotFoundException notFoundException => (404, notFoundException.Message),
                _ => default
            };

            if (statusCode == default)
            {
                return false;
            }

            _logger.LogError(exception, errorMessage ?? "An error occurred.");

            httpContext.Response.StatusCode = statusCode;
            if (errorMessage != null)
            {
                await httpContext.Response.WriteAsJsonAsync(new { error = errorMessage });
            }

            return true;
        }

    }
}
