namespace ServicesExample.Domain.Models;

public class AuthorDto : UserDto
{
    public string Name { get; set; } = null!;
    public List<EventDto> Events { get; set; } = [];
}