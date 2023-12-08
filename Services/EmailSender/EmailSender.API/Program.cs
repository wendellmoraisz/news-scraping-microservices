using EmailSender.API.EventBusConsumer;
using EmailSender.Application.Mappers;
using EmailSender.Application.Services;
using EmailSender.Core.Entities;
using EmailSender.Infrastructure.Services;
using EventBus.Messages.Common;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISendEmailService, SendEmailService>();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));
builder.Services.AddScoped<ISendEmailService, SendEmailService>();
builder.Services.AddAutoMapper(typeof(SendEmailMapper));
builder.Services.AddScoped<SendEmailConsumer>();
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<SendEmailConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstants.SendEmailQueue, c =>
        {
            c.ConfigureConsumer<SendEmailConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.Run();