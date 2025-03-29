using System.Text.Json;
using AutoMapper;
using ServicesExample.Abstractions;
using ServicesExample.Entities;
using ServicesExample.Models;

namespace ServicesExample.Services;

public class EventRepository(string path, IMapper mapper) : IEventRepository
{
    private readonly List<Event> _events = 
        JsonSerializer.Deserialize<List<Event>>(File.ReadAllText(path)) 
        ?? [];
    public Task<IEnumerable<EventDto>> GetAllAsync()
    {
        return Task.FromResult(mapper.Map<IEnumerable<EventDto>>(_events));
    }

    public async Task<EventDto> AddAsync(EventDto ev)
    {
        ev.Id = Guid.NewGuid();
        _events.Add(mapper.Map<Event>(ev));
        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(_events));
        return ev;
    }

    public async Task UpdateAsync(EventDto evtDto)
    {
        var evt = mapper.Map<Event>(evtDto);
        var index = _events.FindIndex(x => x.Id == evt.Id);
        if (index == -1)
        {
            return;
        }
        _events[index] = evt;
        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(_events));
    }

    public Task<EventDto> GetAsync(Guid eventId)
    {
        return Task.FromResult(
                   mapper.Map<EventDto>(
                       _events.FirstOrDefault(x => x.Id == eventId)))
               ?? throw new KeyNotFoundException();
    }
}