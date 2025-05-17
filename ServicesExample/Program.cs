using System.Text;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServicesExample.Api;
using ServicesExample.Api.Endpoints;
using ServicesExample.Api.Pipeline;
using ServicesExample.Configurations.Mapper;
using ServicesExample.Configurations.Options;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Services;
using ServicesExample.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ScoreEndedEventsByLastDayJob>();

builder.Services.AddHangfire(builder.Configuration["App:ConnectionString"]!);

builder.Services.AddLogging(builder.Logging);

builder.Services.AddDatabase(builder.Configuration["App:ConnectionString"]!);

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
builder.Services.AddScoped<JwtWorker>();

builder.Services.AddAuthorization();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("AuthConfig"));
builder.Services.AddSwagger(builder.Environment.ApplicationName);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddQuotes(builder.Configuration);

builder.Services.AddAutoMapper(expression =>
{
    expression.AddProfile<EventMapperProfile>();
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard();

var apiGroup = app.MapGroup("api");
apiGroup.MapEvents();
apiGroup.MapAuthors();
apiGroup.MapUsers();
apiGroup.MapStudents();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
        $"{builder.Environment.ApplicationName} v1"));
}

RecurringJob.AddOrUpdate("update-student", 
    (ScoreEndedEventsByLastDayJob scoreEndedEventsByLastDayJob) =>
        scoreEndedEventsByLastDayJob.UpdateScoreAllStudentsByLastDay(),
    Cron.Daily(0,0));

app.UseHttpLogging();

app.Run();