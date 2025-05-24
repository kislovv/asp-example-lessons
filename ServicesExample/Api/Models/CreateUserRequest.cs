using ServicesExample.Domain.Entities;

namespace ServicesExample.Api.Models;

public class CreateUserRequest
{
    public string Name { get; set; } = null!;
    public required string Login { get; set; }
    public required string Password { get; set; }
    public UserRole Role { get; set; }
}