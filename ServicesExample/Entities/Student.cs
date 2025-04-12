namespace ServicesExample.Entities;

public class Student: User
{
    public required string Name { get; set; }
    public required uint Course { get; set; }
    public required uint Score { get; set; } = 0;
    public required string Group { get; set; }
    public required string Institute { get; set; }

    public List<Event> Events { get; set; } = [];
}