namespace ServicesExample.Domain.Entities;

public class Author: User
{
    public required string Name { get; set; }
    public List<Event> Events { get; set; } = [];
}