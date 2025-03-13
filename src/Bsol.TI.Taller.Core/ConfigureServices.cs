using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Bsol.TI.Taller.Core;
public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
public class CoreAssembly { }
