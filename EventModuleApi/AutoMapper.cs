using AutoMapper;

namespace EventModuleApi;
public class AutoMapper : Profile
{
    public AutoMapper()
    {

        CreateMap<EventDto, Event>();
        CreateMap<Event, EventDto>();

        CreateMap<Event, CreateEventDto>();
        CreateMap<CreateEventDto, Event>();

    }
}