using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Rested.Core.MediatR;

public static class Extensions
{
    public static IServiceCollection AddMediatRRested(this IServiceCollection services)
    {
        services
            .AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetEntryAssembly());
            });

        return services;
    }
}