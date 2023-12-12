using System.Net;
using EmailManager.API.Extensions;
using EmailManager.API.Services;
using EmailManager.Application.Common.Behaviors;
using EmailManager.Application.Repositories;
using EmailManager.Application.UseCases.CreateEmailAddress;
using EmailManager.Application.UseCases.GetAllEmails;
using EmailManager.Infrastructure.Data;
using EmailManager.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 80, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });
    serverOptions.ListenAnyIP(50055, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddScoped<IEmailContext, EmailContext>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(GetAllEmailsHandler).Assembly);
});
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(CreateEmailAddressMapper));
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration["DatabaseSettings:ConnectionString"], "Emails Mongo Db Health Check",
        HealthStatus.Degraded);
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmailAddressValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<EmailService>();
app.MapControllers();
app.UseRouting();
app.UseErrorHandler();

app.Run();