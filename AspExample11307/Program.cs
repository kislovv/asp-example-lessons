using System.Globalization;
using AspExample11307;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<LoggerMiddleware>();
builder.Services.AddRazorPages(options =>
{
    options.RootDirectory = "/Pages";
});
var app = builder.Build();


app.UseStaticFiles();
app.MapRazorPages();

app.MapPost("/updateDate", async context =>
{
    await context.Response.WriteAsJsonAsync(new
    {
        date = DateTime.Now.ToString(CultureInfo.InvariantCulture)
    });
});

app.Run();