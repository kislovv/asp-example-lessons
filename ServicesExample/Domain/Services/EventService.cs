using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Services;

public class EventService(IEventRepository eventRepository, IQuotesService quotesService) : IEventService
{
    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var events = await eventRepository.GetAllAsync();
        
        return  events;
    }

    public async Task<EventDto> CreateEventAsync(EventDto dto)
    {
        var quotaResult = await quotesService.GetRandomSloganAsync();
        dto.Slogan = quotaResult;
        
        var result = await eventRepository.AddAsync(dto);
        
        return result;
    }

    public async Task<bool> RegistrationEventAsync(Guid eventId)
    {
        var ev = await eventRepository.GetByIdAsync(eventId);

        if (ev == null || ev.Quota < 1)
        {
            return false;
        }
        ev.Quota--;
        await eventRepository.UpdateAsync(ev);
        
        return true;
    }
}