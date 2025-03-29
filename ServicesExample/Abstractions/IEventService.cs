using ServicesExample.Models;

namespace ServicesExample.Abstractions;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto> CreateEventAsync(EventDto dto);
    Task<bool> RegistrationEventAsync(Guid eventId);
}