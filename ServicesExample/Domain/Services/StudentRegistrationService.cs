using Microsoft.AspNetCore.Identity;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Services;

public class StudentRegistrationService(IPasswordHasher<UserDto> passwordHasher, 
    IStudentRepository userRepository): 
    IUserRegistrationService
{
    public async Task<Result<UserDto>> RegistrationUser(UserDto user)
    {
        var hashedPassword = passwordHasher.HashPassword(user, user.Password);
        user.Password = hashedPassword;
        var result = await userRepository.AddAsync((StudentDto)user);
        
        return Result<UserDto>.Success(result);
    }
}