using ServicesExample.Abstractions;
using ServicesExample.Models;

namespace ServicesExample.Services;

public class EventService(IEventRepository eventRepository, IQuotesService quotesService) : IEventService
{
    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var events = await eventRepository.GetAllAsync();
        
        return  events;
    }

    public async Task<EventDto> CreateEventAsync(EventDto dto)
    {
        var quotaResult = await quotesService.GetRandomQuotesAsync();
        dto.Slogan = quotaResult.Content;
        
        var result = await eventRepository.AddAsync(dto);
        
        return result;
    }

    public async Task<bool> RegistrationEventAsync(Guid eventId)
    {
        var ev = await eventRepository.GetByIdAsync(eventId);

        if (ev.Quota < 1)
        {
            return false;
        }
        ev.Quota--;
        await eventRepository.UpdateAsync(ev);
        
        return true;
    }
}