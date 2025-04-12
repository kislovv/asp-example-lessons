using AutoMapper;
using ServicesExample.Api.Models;
using ServicesExample.Domain.Entities;
using ServicesExample.Domain.Models;

namespace ServicesExample.Configurations.Mapper;

public class EventMapperProfile : Profile
{
    public EventMapperProfile()
    {
        CreateMap<CreateEventRequest, EventDto>()
            .ForMember(dto => dto.DateTimeOfStart, 
                opt => opt.MapFrom(src => src.Start))
            .ForMember(dto => dto.DateTimeOfEnd, 
                opt => opt.MapFrom(src => src.End))
            .ForMember(dto => dto.AuthorId, 
                opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dto => dto.Author, opt => opt.Ignore())
            .ForMember(dto => dto.Students, opt => opt.Ignore());
        
        CreateMap<EventDto, Event>()
            .ReverseMap();
    }
}