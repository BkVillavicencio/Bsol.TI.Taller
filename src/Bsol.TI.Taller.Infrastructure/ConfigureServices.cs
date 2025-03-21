using System.Reflection;
using Bsol.TI.Taller.Infrastructure.Data;
using Bsol.TI.Taller.SharedKernel;
using Bsol.TI.Taller.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Bsol.TI.Taller.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, Assembly? callingAssembly = null)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // Use for Specification Repository
        services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddTransient(typeof(IReadRepository<>), typeof(EfRepository<>));
        // Use for Domaint Events
        services.AddTransient<IMediator, Mediator>( );
        services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(GetAssemblies(callingAssembly)));

        //services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
        services.AddMemoryCache();
        services.AddScoped(typeof(ICommandBaseRepository<>), typeof(CommandRepository<>));
        return services;
    }

    private static Assembly[] GetAssemblies(Assembly? callingAssembly)
    {
        var _assemblies = new List<Assembly>();
        var coreAssembly =
          Assembly.GetAssembly(typeof(Bsol.TI.Taller.Core.CoreAssembly));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(AppDbContext));
        if (coreAssembly != null)
        {
            _assemblies.Add(coreAssembly);
        }

        if (infrastructureAssembly != null)
        {
            _assemblies.Add(infrastructureAssembly);
        }

        if (callingAssembly != null)
        {
            _assemblies.Add(callingAssembly);
        }
        return [.. _assemblies];
    }
}
