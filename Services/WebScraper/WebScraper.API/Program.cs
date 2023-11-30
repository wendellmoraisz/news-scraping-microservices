using WebScraper.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
var app = builder.Build();

app.Run();