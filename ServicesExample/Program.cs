using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServicesExample.Api.Endpoints;
using ServicesExample.Api.Models;
using ServicesExample.Configurations.Mapper;
using ServicesExample.Configurations.Swagger;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Entities;
using ServicesExample.Domain.Services;
using ServicesExample.Infrastructure.Database;
using ServicesExample.Infrastructure.QuotesSystem;
using ServicesExample.Infrastructure.QuotesSystem.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSnakeCaseNamingConvention();
    optionsBuilder.UseNpgsql(builder.Configuration["App:ConnectionString"]);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Audience = builder.Configuration["AuthConfig:Audience"];
        options.ClaimsIssuer = builder.Configuration["AuthConfig:Issuer"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["AuthConfig:Issuer"],
            ValidAudience = builder.Configuration["AuthConfig:Audience"],
            RequireExpirationTime = true,
            RequireAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AuthConfig:IssuerSignKey"]!)),
            
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName,
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


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


app.UseAuthentication();
app.UseAuthorization();


var apiGroup = app.MapGroup("api");

apiGroup.MapGroup("user").WithTags("Users").MapPost("auth", async (UserLoginModel userLoginModel, AppDbContext db, HttpContext context) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Login == userLoginModel.Login &&
                                                       u.Password == userLoginModel.Password);
    if (user == null)
    {
        return Results.Unauthorized();
    }

    var claims = new List<Claim>
    {
        new (ClaimTypes.Role, user.Role),
        new (ClaimTypes.Name, user.Login)
    };
    
    var jwt = new JwtSecurityToken(
        issuer: builder.Configuration["AuthConfig:Issuer"],
        audience: builder.Configuration["AuthConfig:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(
            builder.Configuration.GetValue<int>("AuthConfig:ExpiredAtMinutes")),
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AuthConfig:IssuerSignKey"]!)),
            SecurityAlgorithms.HmacSha256));
    
    return Results.Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
});

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