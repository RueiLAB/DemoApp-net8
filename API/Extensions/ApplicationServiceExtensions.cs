using System;
using API.Data;
using API.Repository;
using API.Repository.Interfaces;
using API.Services;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config)
    {
        services.AddControllers();
        // 註冊 EF DbContext
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        // 註冊 CORS 服務，用於支援跨來源請求
        services.AddCors(options => 
        {
            options.AddPolicy("AllowAll",
            policy => 
            {
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:5000")
                    .WithExposedHeaders("Content-Type");
            });
        });
        // 註冊 Repository 與 Service
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}
