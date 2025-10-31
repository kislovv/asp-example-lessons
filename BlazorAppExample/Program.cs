using BlazorAppExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient("quotes",client =>
{
    client.BaseAddress = new Uri("https://api.api-ninjas.com");
    client.DefaultRequestHeaders.Add("X-API-Key", "zV3MLERI3/LIKlGWPXoeKQ==w55oaGmxBTqPJ24n");
});

var app = builder.Build();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAntiforgery();

app.Run();