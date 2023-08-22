namespace EventModuleApi.Core.Contracts;
public interface IEventService
{
   Task<ServiceResponse<EventDto>> GetEvent(string eventId);
   Task<PagedResponse<IEnumerable<EventDto>>> GetAllEvent(PaginatedReq paginatedReq);
   Task<ServiceResponse<string>> CreateEvent(CreateEventDto createEventDto);
   Task<ServiceResponse<string>> DeleteEvent(string eventId);

}
