namespace ServicesExample.Entities;

public abstract class User
{
    public long? Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}