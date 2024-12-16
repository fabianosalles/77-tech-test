using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LMS.BookInventory.Application;

/// <summary>
/// Static class containing dependencies for the application layer
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    /// <summary>
    /// Configure all application services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
                
        services.AddMediatR(delegate(MediatRServiceConfiguration config)
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());            
        });

        return services;
    }
}
