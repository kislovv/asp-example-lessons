using AutoMapper;
using ServicesExample.Api.Models;
using ServicesExample.Domain.Abstractions;
using ServicesExample.Domain.Models;

namespace ServicesExample.Api.Endpoints;

public static class EventEndpointsExt
{
    public static IEndpointRouteBuilder MapEvents(this IEndpointRouteBuilder endpoints)
    {
        
        var eventGroup = endpoints.MapGroup("/events").WithTags("Events");
        
        eventGroup.MapGet("/get-all-events", async (IEventService service) =>
        {
            var list = await service.GetAllEventsAsync();
            return Results.Ok(list);
        }).WithSummary("Get all events")
            .Produces<List<EventDto>>();

        eventGroup.MapPost("/add-event", async (IEventService service, 
            IMapper mapper, CreateEventRequest createEventReq) =>
        {
            var dto = mapper.Map<EventDto>(createEventReq);
            var result = await service.CreateEventAsync(dto);
            return Results.Ok(result);
        }).Produces<EventDto>();
        
        return eventGroup;
    }
}