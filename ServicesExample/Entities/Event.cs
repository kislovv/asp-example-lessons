namespace ServicesExample.Entities;

public class Event
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Place { get; set; }
    public required string Author { get; set; }
    public required DateTime DateTimeOfStart { get; set; }
    public required DateTime DateTimeOfEnd { get; set; }
    public required uint Quota { get; set; }
    public string? Slogan { get; set; }
    //TODO: Students, Score
}