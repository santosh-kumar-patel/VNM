using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Middleware
{
    public class ApiExpiryMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly DateTime _expiryDate = new DateTime(2025, 9, 2); // Set your cutoff

        public ApiExpiryMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;

            int.TryParse(_config["APIExpirySetting:ExpiryDay"], out int ExpiryDay);
            _expiryDate = DateTime.UtcNow.AddDays(ExpiryDay);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;
            int.TryParse(_config["APIExpirySetting:version"], out int version);
            if (path.StartsWith("/api/v"+ version) && DateTime.Now > _expiryDate)
            {
                context.Response.StatusCode = StatusCodes.Status410Gone; // HTTP 410: Gone
                await context.Response.WriteAsync("API v" + version + " is no longer available.");
                return;
            }

            await _next(context);
        }
    }
}
