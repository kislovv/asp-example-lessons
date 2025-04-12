namespace ServicesExample.Domain.Models;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<EventDto> Events { get; set; } = [];
}