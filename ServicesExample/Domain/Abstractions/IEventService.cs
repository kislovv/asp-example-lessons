﻿using ServicesExample.Domain.Models;

namespace ServicesExample.Domain.Abstractions;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto> CreateEventAsync(EventDto dto);
    Task<bool> RegistrationEventAsync(Guid eventId);
}