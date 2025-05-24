using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IJwtTokenGenerator
{
    string? CreateJwtToken(UserDto userDto);
}