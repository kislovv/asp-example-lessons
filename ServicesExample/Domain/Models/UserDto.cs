using ServicesExample.Domain.Entities;

namespace ServicesExample.Domain.Models;

public class UserDto
{
    public long? Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public UserRole Role { get; set; }
}