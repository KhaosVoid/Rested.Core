using Microsoft.Extensions.DependencyInjection;
using Rested.Core.CQRS.Data;
using System.Reflection;

namespace Rested.Core.Data
{
    public static class Extensions
    {
        public static IServiceCollection RegisterProjectionMappings(this IServiceCollection services) =>
            services.AddSingleton(new ProjectionRegistration());

        public static IServiceCollection RegisterProjectionMappingsFromAssembly(this IServiceCollection services, Assembly assembly) =>
            services.AddSingleton(new ProjectionRegistration(assembly));

        public static IServiceCollection RegisterProjectionMappingsFromAssemblies(this IServiceCollection services, Assembly[] assemblies) =>
            services.AddSingleton(new ProjectionRegistration(assemblies));
    }
}
