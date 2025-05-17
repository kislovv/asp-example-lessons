using System.Reflection;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ServicesExample.Configurations.Logging;
using ServicesExample.Configurations.Swagger;

namespace ServicesExample.Api;

public static class ServicesCollectionExt
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, string appName)
    {
        return services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = appName,
                Version = "v1" });
    
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    
            c.OperationFilter<ApiKeyOperationFilter>();
    
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "This is a JWT bearer authentication scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
    
        });
    }

    public static IServiceCollection AddHangfire(this IServiceCollection services, string connectionString)
    {
        services.AddHangfire(configuration =>
        {
            configuration.UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(connectionString);
            });
        });
        
        return services.AddHangfireServer();
    }
    
    public static IServiceCollection AddLogging(this IServiceCollection services, ILoggingBuilder loggingBuilder)
    {
        services.AddTransient(typeof(ILogger<>), typeof(SecureLogger<>));
        
        loggingBuilder.AddSeq();
        
        return services.AddHttpLogging(options =>
        {
            options.ResponseBodyLogLimit = 4096;
        } );
    }
}