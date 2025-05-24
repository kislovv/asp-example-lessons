using Microsoft.EntityFrameworkCore;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Infrastructure.Database;
using ServicesExample.Infrastructure.Database.Authors;
using ServicesExample.Infrastructure.Database.Events;
using ServicesExample.Infrastructure.Database.Students;
using ServicesExample.Infrastructure.QuotesSystem;
using ServicesExample.Infrastructure.QuotesSystem.Options;

namespace ServicesExample.Infrastructure;

public static class ServiceCollectionExt
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        
        return services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseNpgsql(connectionString);
        });
    }

    public static IServiceCollection AddQuotes(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IQuotesService, QuotesService>(client =>
        {
            client.BaseAddress = new Uri(configuration["QuotesService:BaseUrl"]!);
            client.DefaultRequestHeaders.Add(configuration["QuotesService:XHostHeader"]!, 
                configuration["QuotesService:XHostValue"]!);
            client.DefaultRequestHeaders.Add(configuration["QuotesService:XKeyHeader"]!,
                configuration["QuotesService:XKeyValue"]!);
        });

        return services.Configure<QuotesOptions>(configuration.GetSection("QuotesOptions"));
    }
}