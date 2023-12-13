using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});

var app = builder.Build();

app.UseRouting();
app.UseCors("CorsPolicy");
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello Ocelot");
});
await app.UseOcelot();

app.Run();