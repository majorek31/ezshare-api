using Microsoft.Extensions.DependencyInjection;

namespace EzShare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(assembly);
        });
        return services;
    }
}