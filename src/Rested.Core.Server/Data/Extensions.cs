using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rested.Core.Data;
using System.Reflection;

namespace Rested.Core.Server.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddRestedRouteTemplates(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            RestedRouteTemplateSettings.InitializeRouteTemplateSettings(configuration);

            return services;
        }

        public static IServiceCollection RegisterProjectionMappings(this IServiceCollection services) =>
            services.AddSingleton(ProjectionRegistration.Initialize());

        public static IServiceCollection RegisterProjectionMappingsFromAssembly(this IServiceCollection services, Assembly assembly) =>
            services.AddSingleton(ProjectionRegistration.Initialize(assembly));

        public static IServiceCollection RegisterProjectionMappingsFromAssemblies(this IServiceCollection services, Assembly[] assemblies) =>
            services.AddSingleton(ProjectionRegistration.Initialize(assemblies));
    }
}
