namespace ServicesExample.Models;

public class EventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Place { get; set; }
    public string? Author { get; set; }
    public DateTime DateTimeOfStart { get; set; }
    public DateTime DateTimeOfEnd { get; set; }
    public uint Quota { get; set; }
    public string? Slogan { get; set; }
}