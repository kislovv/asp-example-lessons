using AutoMapper;
using ServicesExample.Entities;
using ServicesExample.Models;

namespace ServicesExample.Configurations.Mapper;

public class EventMapperProfile : Profile
{
    public EventMapperProfile()
    {
        CreateMap<CreateEventRequest, EventDto>()
            .ForMember(dto => dto.DateTimeOfStart, opt =>
            {
                opt.MapFrom(src => src.Start);
            })
            .ForMember(dto => dto.DateTimeOfEnd, opt =>
            {
                opt.MapFrom(src => src.End);
            });
        CreateMap<EventDto, Event>().ReverseMap();
    }
}