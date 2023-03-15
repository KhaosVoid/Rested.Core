using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Rested.Core.Http
{
    public class IfMatchByteArrayOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameters = context.MethodInfo
                .GetParameters()
                .Where(parameterInfo => parameterInfo.ParameterType == typeof(IfMatchByteArray));

            foreach (ParameterInfo parameterInfo in parameters)
            {
                if (context.SchemaRepository.TryLookupByType(typeof(IfMatchByteArray), out var schemaId))
                {
                    var openApiParameter = operation.Parameters.FirstOrDefault(
                        p => p.Name == parameterInfo.Name &&
                        p.Schema.Reference?.Id == schemaId.Reference?.Id);

                    if (openApiParameter is not null)
                    {
                        openApiParameter.In = ParameterLocation.Header;
                        openApiParameter.Name = HeaderNames.IfMatch;
                        openApiParameter.Required = true;
                        openApiParameter.Description = "The ETag (entity tag) of the resource.";
                        openApiParameter.Schema = new OpenApiSchema()
                        {
                            Type = "string"
                        };
                    }
                }
            }
        }
    }
}
