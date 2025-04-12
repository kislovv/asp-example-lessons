using System.Text.Json;
using Microsoft.Extensions.Options;
using ServicesExample.Abstractions;
using ServicesExample.Api.Configurations.Options;
using ServicesExample.Api.Models;

namespace ServicesExample.Domain.Services;

public class QuotesService(HttpClient client, IOptionsMonitor<QuotesOptions> quotesOptions) : IQuotesService
{
    public async Task<QuotesApiResponse> GetRandomQuotesAsync()
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

        return result;
    }
}