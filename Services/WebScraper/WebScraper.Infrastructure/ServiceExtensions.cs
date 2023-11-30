using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebScraper.Application.Repositories;
using WebScraper.Application.Services;
using WebScraper.Infrastructure.Context;
using WebScraper.Infrastructure.Repositories;
using WebScraper.Infrastructure.Services;

namespace WebScraper.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NewsDatabase");
        services.AddDbContext<DataContext>(opts =>
            opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<IScrapingService, ScrapingService>();
    }
}