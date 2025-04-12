using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ServicesExample.Api.Endpoints;
using ServicesExample.Api.Models;
using ServicesExample.Configurations.Mapper;
using ServicesExample.Configurations.Swagger;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Entities;
using ServicesExample.Domain.Models;
using ServicesExample.Infrastructure.Database;
using ServicesExample.Infrastructure.QuotesSystem;
using ServicesExample.Infrastructure.QuotesSystem.Options;
using ServicesExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSnakeCaseNamingConvention();
    optionsBuilder.UseNpgsql(builder.Configuration["App:ConnectionString"]);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName,
        Version = "v1" });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    
    c.OperationFilter<ApiKeyOperationFilter>();
    
});
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddHttpClient<IQuotesService, QuotesService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["QuotesService:BaseUrl"]!);
    client.DefaultRequestHeaders.Add(builder.Configuration["QuotesService:XHostHeader"]!, 
        builder.Configuration["QuotesService:XHostValue"]!);
    client.DefaultRequestHeaders.Add(builder.Configuration["QuotesService:XKeyHeader"]!,
        builder.Configuration["QuotesService:XKeyValue"]!);
});

builder.Services.Configure<QuotesOptions>(builder.Configuration.GetSection("QuotesOptions"));

builder.Services.AddAutoMapper(expression =>
{
    expression.AddProfile<EventMapperProfile>();
});

var app = builder.Build();



var apiGroup = app.MapGroup("api");

apiGroup.MapEvents();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
        $"{builder.Environment.ApplicationName} v1"));
    
    app.Map("/", (IConfiguration appConfig) =>
        $"{string.Join("\r\n",appConfig.AsEnumerable().Select(x=> $"{x.Key} : {x.Value} "))} ");
}

apiGroup.MapGroup("authors").WithTags("Authors").MapPost("/add", 
    async (AppDbContext dbContext, CreateAutorRequest authorDto) =>
{
    await dbContext.Authors.AddAsync(new Author
    {
        Name = authorDto.Name,
        Login = authorDto.Login,
        Password = authorDto.Password,
        Role = "Author"
    });
    
    await dbContext.SaveChangesAsync();
    
    return Results.Ok();
});


app.Run();