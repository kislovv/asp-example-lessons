namespace ServicesExample.Domain.Entities;

public class Event
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Place { get; set; }
    public required DateTime DateTimeOfStart { get; set; }
    public required DateTime DateTimeOfEnd { get; set; }
    public required uint Quota { get; set; }
    public required uint Score { get; set; }
    
    public required long AuthorId { get; set; }
    public Author? Author { get; set; }

    public List<Student> Students { get; set; } = [];
    
    public bool IsDeleted { get; set; }
}