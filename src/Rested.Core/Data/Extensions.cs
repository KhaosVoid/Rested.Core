using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;

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

        /// <summary>
        /// Converts a string to camelcase. Supports strings using dot notation (e.g. <c>object.myVariable</c>)
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            var sections = value.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sections?.Length; i++)
                sections[i] = JsonNamingPolicy.CamelCase.ConvertName(sections[i]);

            return string.Join(".", sections);
        }
    }
}
