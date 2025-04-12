namespace ServicesExample.Api.Models;

public class CreateAutorRequest
{
    public string Name { get; set; } = null!;
    public required string Login { get; set; }
    public required string Password { get; set; }
}