using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IAuthService
{
    Task<Result<string>> AuthenticateAsync(string login, string password);
}