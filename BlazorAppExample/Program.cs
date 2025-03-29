using BlazorAppExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("quotes",client =>
{
    client.BaseAddress = new Uri("https://quotes15.p.rapidapi.com");
    client.DefaultRequestHeaders.Add("x-rapidapi-host", "quotes15.p.rapidapi.com");
    client.DefaultRequestHeaders.Add("x-rapidapi-key", "11c5aa0932msh6f977a8bfc17035p154120jsn86e885da8be0");
});

var app = builder.Build();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAntiforgery();

app.Run();