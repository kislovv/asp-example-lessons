namespace ServicesExample.Domain.Models;

public class EventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Place { get; set; } = null!;
    public AuthorDto? Author { get; set; }
    public int? AuthorId { get; set; }
    public DateTime DateTimeOfStart { get; set; }
    public DateTime DateTimeOfEnd { get; set; }
    public uint Quota { get; set; }
    public uint Score { get; set; }
    public List<StudentDto> Students { get; set; } = [];
    
    public string? Slogan { get; set; }
}