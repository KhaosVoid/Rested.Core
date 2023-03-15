using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Rested.Core.Http
{
    public static class Extensions
    {
        public static IServiceCollection AddControllersRested(this IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(setupAction =>
                {
                    setupAction.OperationFilter<IfMatchByteArrayOperationFilter>(Array.Empty<object>());
                })
                .AddSwaggerGenNewtonsoftSupport()
                .AddControllersWithViews(options =>
                {
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                })
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.Converters.Add(
                        new StringEnumConverter(
                            namingStrategy: new CamelCaseNamingStrategy()));

                    setupAction.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    setupAction.SerializerSettings.Formatting = Formatting.Indented;

                    setupAction.UseCamelCasing(processDictionaryKeys: true);
                });

            return services;
        }
    }
}
