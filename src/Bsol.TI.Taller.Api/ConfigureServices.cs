using System.Reflection;
using Bsol.TI.Taller.Api.Extensions;
using Bsol.TI.Taller.Api.Health;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace Bsol.TI.Taller.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        string? Url = configuration.GetValue<string>(".Options:Url");
        services.AddHealthChecks()
            .AddCheck("Example Core Health Check", new HttpHealthCheck(Url), HealthStatus.Unhealthy, new[] { "example-core" })
            ;

        services.AddTransient<Middleware.ExceptionHandlingMiddleware>();
        services.AddApplicationInsightsTelemetry();
        services.Configure<TelemetryConfiguration>((o) =>
        {
            o.TelemetryInitializers.Add(new AppInsightsTelemetryInitializer());
        });
        return services;
    }
}
