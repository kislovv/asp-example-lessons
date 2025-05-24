using Microsoft.AspNetCore.Identity;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Services;

public class AuthService(
    IPasswordHasher<UserDto> passwordHasher,
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator)
    : IAuthService
{
    public async Task<Result<string>> AuthenticateAsync(string login, string password)
    {
        var user = await userRepository.GetByLogin(login);
        if (user is null)
        {
            return Result<string>.Failure("Invalid login");
        }

        if (passwordHasher.VerifyHashedPassword(user, password, user.Password) !=
            PasswordVerificationResult.Success)
        {
            return Result<string>.Failure("Invalid password");
        }

        var token = jwtTokenGenerator.CreateJwtToken(user);
        return Result<string>.Success(token!);
    }
}