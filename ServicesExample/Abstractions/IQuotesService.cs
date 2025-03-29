using ServicesExample.Models;

namespace ServicesExample.Abstractions;

public interface IQuotesService
{
    Task<QuotesApiResponse> GetRandomQuotesAsync();
}