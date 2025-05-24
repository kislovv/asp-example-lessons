using System.Reflection;
using System.Text;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServicesExample.Api.Pipeline;
using ServicesExample.Configurations.Logging;
using ServicesExample.Configurations.Options;
using ServicesExample.Configurations.Swagger;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Services;

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

    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtTokenGenerator, JwtWorker>();
        services.Configure<JwtOptions>(configuration.GetSection("AuthConfig"));
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = configuration["AuthConfig:Audience"];
                options.ClaimsIssuer = configuration["AuthConfig:Issuer"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["AuthConfig:Issuer"],
                    ValidAudience = configuration["AuthConfig:Audience"],
                    RequireExpirationTime = true,
                    RequireAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["AuthConfig:IssuerSignKey"]!))
                };
            });
        
        return services.AddAuthorization();
    }
}