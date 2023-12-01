using Quartz;
using WebScraper.Application.UseCases;

namespace WebScraper.Application.ScheduledJobs;

public static class ServiceExtensions
{
    public static void ConfigureScheduledJobs(this IServiceCollection services)
    {
        services.AddScoped<SendScrapedNews>();
        services.AddScoped<ScrapNewsScheduler>();

        services.AddQuartz();
        
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    }
}