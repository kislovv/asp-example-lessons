using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServicesExample.Configurations.Swagger;

public class ApiKeyOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(new OpenApiParameter
        {
            In = ParameterLocation.Header, 
            Name = "accept-language",
            Description = "pass the locale here: examples like => ar,ar-jo,en,en-gb",
            Schema = new OpenApiSchema
            {
                Type = "String"
            }
        });
        operation.Parameters.Add(new OpenApiParameter
        {
            In = ParameterLocation.Header, 
            Name = "X-API-KEY",
            Description = "pass api key here",
            Schema = new OpenApiSchema
            {
                Type = "String"
            },
            Required = true
        });
    }
}