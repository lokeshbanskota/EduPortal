using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EduPortal.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                LogExceptionDetails(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Details = exception.InnerException?.Message
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        private void LogExceptionDetails(Exception ex)
        {
            var stackTrace = new StackTrace(ex, true); // Capture file info
            var frame = stackTrace.GetFrame(0); // Get the first frame of the stack trace

            var fileName = frame?.GetFileName() ?? "Unknown File";
            var lineNumber = frame?.GetFileLineNumber() ?? 0;
            //var methodName = frame?.GetMethod()?.Name ?? "Unknown Method";

            _logger.LogError(
                ex,
                "Exception occurred in {FileName} at line {LineNumber} Error: {ErrorMessage}",
                fileName, lineNumber, ex.Message
            );
        }
    }

}
