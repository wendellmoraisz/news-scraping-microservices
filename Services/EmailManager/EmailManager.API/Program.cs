using System.Reflection;
using EmailManager.API.Services;
using EmailManager.Application.Repositories;
using EmailManager.Application.UseCases.GetAllEmails;
using EmailManager.Infrastructure.Data;
using EmailManager.Infrastructure.Repositories;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailContext, EmailContext>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(GetAllEmailsHandler).Assembly);
});
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration["DatabaseSettings:ConnectionString"], "Emails Mongo Db Health Check",
        HealthStatus.Degraded);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<EmailService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(
            "Communication with gRPC endpoints must be made through a gRPC client.");
    });
});

app.Run();