using System.Text.Json;
using Microsoft.Extensions.Options;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Infrastructure.QuotesSystem.Options;

namespace ServicesExample.Infrastructure.QuotesSystem;

public class QuotesService(HttpClient client, IOptionsMonitor<QuotesOptions> quotesOptions) : IQuotesService
{
    public async Task<string> GetRandomSloganAsync()
    {
        var options  = quotesOptions.CurrentValue;
        var result = await client.GetFromJsonAsync<QuotesApiResponse>(
            $"/quotes/random/?language_code={options.LanguageCode}", 
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });
        
        if (result == null)
        {
            throw new ApplicationException("No quotes available");
        }

        return result.Content ?? string.Empty;
    }
}