using System.Reflection;
using AutoMapper;
using ServicesExample.Abstractions;
using ServicesExample.Configurations.Mapper;
using ServicesExample.Configurations.Options;
using ServicesExample.Configurations.Swagger;
using ServicesExample.Endpoints;
using ServicesExample.Models;
using ServicesExample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository>(provider =>
{
    var mapper = provider.GetRequiredService<IMapper>();
    return new EventRepository(builder.Configuration["EventsPath"]!, mapper);
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


app.Run();