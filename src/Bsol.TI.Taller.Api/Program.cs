using Bsol.TI.Taller.Api;
using Bsol.TI.Taller.Api.Extensions;
using Bsol.TI.Taller.Api.Middleware;
using Bsol.TI.Taller.Core;
using Bsol.TI.Taller.Infrastructure;
using Destructurama;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddFastEndpoints();
builder.Services.AddCustomApiVersioning();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
});
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext(connectionString);
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(app.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.ApplicationInsights(app.Services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
    .Destructure.UsingAttributes()
    .CreateLogger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "Bsol/BusinessApiTaller/v";
    c.Versioning.PrependToRoute = true;
});
app.UseSwaggerGen(); // FastEndpoints middleware

// Exception Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHealthChecks("/health",
    new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

public partial class Program
{
    protected Program() { }
}
