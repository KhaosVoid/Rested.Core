using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Rested.Core.Server.Validation;

public static class Extensions
{
    public static IServiceCollection AddFluentValidationRested(this IServiceCollection services)
    {
        services
            .AddTransient(
                serviceType: typeof(IPipelineBehavior<,>),
                implementationType: typeof(FluentValidationPipelineBehavior<,>))
            .AddTransient<RestedValidationExceptionMiddleware>()
            .AddValidatorsFromAssembly(Assembly.GetEntryAssembly());

        return services;
    }
}