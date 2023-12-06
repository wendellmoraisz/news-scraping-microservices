using EmailManager.Grpc.Protos;
using MassTransit;
using Quartz;
using WebScraper.Application.ScheduledJobs;
using WebScraper.Application.Services;
using WebScraper.Application.Services.GrpcServices;
using WebScraper.Infrastructure;
using WebScraper.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IScrapingService,ScrapingService>();
builder.Services.ConfigureScheduledJobs();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.AddScoped<EmailManagerGrpcService>();
builder.Services.AddGrpcClient<EmailProtoService.EmailProtoServiceClient>
    (o => o.Address = new Uri(builder.Configuration["GrpcSettings:EmailUrl"] ?? string.Empty));
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

var job = JobBuilder.Create<ScrapNewsScheduler>()
    .WithIdentity("send-scraped-news", "news")
    .Build();

var trigger = TriggerBuilder.Create()
    .WithIdentity("send-scraped-news-minutely", "news")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(120)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

app.Run();