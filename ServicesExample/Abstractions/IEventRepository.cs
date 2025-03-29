using ServicesExample.Entities;
using ServicesExample.Models;

namespace ServicesExample.Abstractions;

public interface IEventRepository
{
    Task<IEnumerable<EventDto>> GetAllAsync();
    Task<EventDto> AddAsync(EventDto ev);
    Task UpdateAsync(EventDto ev);
    Task<EventDto> GetAsync(Guid eventId);
}