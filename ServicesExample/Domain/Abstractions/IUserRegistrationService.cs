using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IUserRegistrationService
{
    Task<Result<UserDto>> RegistrationUser(UserDto user);
}