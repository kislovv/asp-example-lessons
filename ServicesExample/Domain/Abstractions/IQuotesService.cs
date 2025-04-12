
namespace ServicesExample.Domain.Abstractions;

public interface IQuotesService
{
    Task<string> GetRandomSloganAsync();
}