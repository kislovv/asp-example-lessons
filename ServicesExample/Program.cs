using FluentValidation;
using Hangfire;
using ServicesExample.Api;
using ServicesExample.Api.Endpoints;
using ServicesExample.Configurations.Mapper;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Entities;
using ServicesExample.Domain.Services;
using ServicesExample.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddKeyedScoped<IUserRegistrationService, 
    StudentRegistrationService>(nameof(UserRole.Student));

builder.Services.AddScoped<ScoreEndedEventsByLastDayJob>();

builder.Services.AddHangfire(builder.Configuration["App:ConnectionString"]!);
builder.Services.AddLogging(builder.Logging);
builder.Services.AddDatabase(builder.Configuration["App:ConnectionString"]!);
builder.Services.AddAuthorization(builder.Configuration);

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