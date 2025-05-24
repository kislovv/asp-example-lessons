using System.Reflection;
using CQRSExample;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSnakeCaseNamingConvention();
    optionsBuilder.UseNpgsql(builder.Configuration["App:ConnectionString"]!);
});

var app = builder.Build();

app.MapPost("/product", async (IMediator mediator, 
    AddProductCommand productCommand) =>
{
    var result = await mediator.Send(productCommand);
    
    return Results.Ok(result);
});

app.Run();