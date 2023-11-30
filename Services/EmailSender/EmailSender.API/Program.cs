using EmailSender.Application.Services;
using EmailSender.Core.Entities;
using EmailSender.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISendEmailService, SendEmailService>();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));

var app = builder.Build();

app.Run();