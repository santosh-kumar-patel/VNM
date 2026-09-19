using BAL.Interface;
using BAL.Service;
using Common.Filters;
using Core.Interfaces;
using Core.Models.Base;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace Common.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:SecretKey"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var error = new
                        {
                            Message = "Token validation failed.",
                            Reason = context.Exception.Message,
                            Timestamp = DateTime.UtcNow,
                            TraceId = context.HttpContext.TraceIdentifier
                        };

                        var json = JsonSerializer.Serialize(error);
                        await context.Response.WriteAsync(json);
                    }
                };
            });

            return services;
        }

        public static IServiceCollection AddSwaggerGeneric(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VNM Web APIs",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.OperationFilter<AuthResponsesOperationFilter>();

            });

            return services;
        }

        public static IServiceCollection AddValidationError(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var response= new Response
                    {
                        Success = false,
                        Message = "Validation failed",
                        StatusCode = StatusCodes.Status400BadRequest,
                        Detail = "One or more validation errors occurred.",
                        Errors = errors,
                        Timestamp = DateTime.UtcNow,
                        TraceId = context.HttpContext.TraceIdentifier
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static IServiceCollection AddThrottling(this IServiceCollection services, IConfiguration config)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("fixed", config =>
                {
                    config.PermitLimit = 5;
                    config.Window = TimeSpan.FromSeconds(30);
                    config.QueueLimit = 2;
                    config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });

            return services;
        }

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IPasswordResetService, PasswordResetService>();
            services.AddTransient<ITokenRefreshService, TokenRefreshService>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();

            return services;
        }

    }
}
