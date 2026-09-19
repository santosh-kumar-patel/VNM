using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VNMAPI;
using DotNetEnv;

Env.Load(".env");
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<Employee, UserRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // 👈 Match your Angular URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services); // calling ConfigureServices method

var app = builder.Build();

app.UseCors("AllowAngularApp");
startup.Configure(app, builder.Environment);


app.Run();

