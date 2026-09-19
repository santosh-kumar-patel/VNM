using BAL.DTOs;
using Core.Models.Base;
using DAL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (BusinessRuleException ex)
            {
                _logger.LogWarning(ex, "Business rule violation");
                await WriteJsonError(context, 400, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteJsonError(context, 500, "An unexpected error occurred.");
            }
        }

        private async Task WriteJsonError(HttpContext context, int statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var error = new Response
            {
                Success = true,
                Message = message,
                StatusCode = statusCode,
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsJsonAsync(error);
        }

    }
}
