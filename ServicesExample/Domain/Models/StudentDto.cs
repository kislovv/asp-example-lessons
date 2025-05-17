namespace ServicesExample.Domain.Models;

public class StudentDto : UserDto
{
    public required string Name { get; set; }
    public uint Course { get; set; }
    public uint Score { get; set; } = 0;
    public string Group { get; set; }
    public string Institute { get; set; }

    public List<EventDto> Events { get; set; } = [];
}