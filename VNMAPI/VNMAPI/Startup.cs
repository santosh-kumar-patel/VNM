using AspNetCoreRateLimit;
using BAL.Interface;
using BAL.Service;
using Common.Filters;
using Common.Middleware;
using Common.Utilities.Extensions;
using Common.Utilities.Helper;
using Core.Exceptions;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace VNMAPI
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //if (!string.IsNullOrEmpty(_configuration["Jwt:SecretKey"]))
            //    throw new Exception(_configuration["Jwt:SecretKey"].ToString()) ;


            services.AddControllers()
                // .AddXmlSerializerFormatters();
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true; // Compress even over HTTPS
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(opts =>
            {
                opts.Level = CompressionLevel.Fastest;
            });

            services.AddHttpClient();
            services.Configure<IpRateLimitOptions>(
            _configuration.GetSection("IpRateLimiting"));

            #region /******* Add Traffic Control Machanism ***********/

            #region ----- Add Rate limmiting ---------------
            services.AddMemoryCache();
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            #endregion

            #region ----- Add Throttling ---------------

            // services.AddThrottling(_configuration); // custom extension
            //services.AddRateLimiter(options =>
            //{
            //    options.AddFixedWindowLimiter("fixed", config =>
            //    {
            //        config.PermitLimit = 5;
            //        config.Window = TimeSpan.FromSeconds(30);
            //        config.QueueLimit = 2;
            //        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            //    });
            //});

            #endregion

            #endregion


            //services.AddTransient<EmployeeService, EmployeeService>();
            //services.AddTransient<UserRoleService, UserRoleService>();
            //services.AddTransient<ITokenService, TokenService>();
            //services.AddTransient<IAuthenticationService, AuthenticationService>();
            //services.AddTransient<IPasswordResetService, PasswordResetService>();
            //services.AddTransient<ITokenRefreshService, TokenRefreshService>();
            //services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddAuthServices(_configuration); // custom extension
            services.AddEndpointsApiExplorer();

            services.AddMvcCore();
            services.AddAuthorization();
            services.AddDataProtection();
            //services.AddSingleton<ICryptographyService, CryptographyService>();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // e.g., v1, v2
                options.SubstituteApiVersionInUrl = true; // ✅ This replaces {version} in Swagger
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGeneric(_configuration); // custom extension
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "VNM Web APIs",
            //        Version = "v1"
            //    });

            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Please enter a valid token",
            //        Name = "Aut
            //        horization",
            //        Type = SecuritySchemeType.Http,
            //        BearerFormat = "JWT",
            //        Scheme = "bearer"
            //    });
            //    c.OperationFilter<AuthResponsesOperationFilter>();

            //});

            //services.AddCors(policyBuilder =>policyBuilder.AddDefaultPolicy(policy =>policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader()));

            services.AddJwtAuthentication(_configuration); // custom extension
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(option =>
            //{
            //    option.RequireHttpsMetadata = false;
            //    option.SaveToken = true;
            //    option.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = _configuration["Jwt:Issuer"],
            //        ValidAudience = _configuration["Jwt:Audience"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]))
            //    };
            //    option.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = async context =>
            //        {
            //            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //            context.Response.ContentType = "application/json";

            //            var error = new
            //            {
            //                Message = "Token validation failed.",
            //                Reason = context.Exception.Message,
            //                Timestamp = DateTime.UtcNow,
            //                TraceId = context.HttpContext.TraceIdentifier
            //            };

            //            var json = JsonSerializer.Serialize(error);
            //            await context.Response.WriteAsync(json);
            //        }
            //    };

            //});



            //Handle Model Validation Errors Consistently
            services.AddValidationError(_configuration); //custom extension
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = context =>
            //    {
            //        var errors = context.ModelState
            //            .Where(x => x.Value.Errors.Count > 0)
            //            .SelectMany(x => x.Value.Errors)
            //            .Select(e => e.ErrorMessage)
            //            .ToList();

            //        var response = new 
            //        {
            //            StatusCode = StatusCodes.Status400BadRequest,
            //            Message = "Validation Error.",
            //            Detail = "One or more validation errors occurred.",
            //            Timestamp = DateTime.UtcNow,
            //            TraceId = context.HttpContext.TraceIdentifier                      
            //        };

            //        return new BadRequestObjectResult(new { response, errors });
            //    };
            //});

            //services.AddHttpClient("ProductService", client =>
            //{
            //    client.BaseAddress = new Uri("https://api.example.com/products");
            //})
            //.AddPolicyHandler(HttpResiliencePolicyProvider.GetCircuitBreakerPolicy());

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();       
                app.UseSwaggerUI(c =>c.SwaggerEndpoint("/swagger/v1/swagger.json", "VNM Web APIs v1"));
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<ApiExpiryMiddleware>();
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseResponseCompression(); // Must be before MVC or static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "MyStaticFiles")),
                RequestPath = "/StaticFiles"
            });
            
            app.UseRouting();

            // 🔐 Add this before authorization
            app.UseIpRateLimiting(); // rate limiting for .NET 6 or earlier

            //  app.UseRateLimiter(); // throttling for .NET 7 or later


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
