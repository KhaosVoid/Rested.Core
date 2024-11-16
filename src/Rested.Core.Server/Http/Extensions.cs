using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Rested.Core.Server.Json;

namespace Rested.Core.Server.Http;

public static class Extensions
{
    public static IServiceCollection AddControllersRested(this IServiceCollection services)
    {
        services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(setupAction =>
            {
                setupAction.OperationFilter<IfMatchByteArrayOperationFilter>();
            })
            .AddControllersWithViews(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                configure.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                configure.JsonSerializerOptions.WriteIndented = true;

                configure.JsonSerializerOptions.Converters.Add(new JsonIFilterConverter());
            });

        return services;
    }
}