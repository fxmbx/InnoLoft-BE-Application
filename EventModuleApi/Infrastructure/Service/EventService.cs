using System.Net;
using AutoMapper;
using EventModuleApi.Core.Contracts;
using EventModuleApi.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;

namespace EventModuleApi.Service;
public class EventService : IEventService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheManager _cacheManager;
    private readonly IMapper _mapper;

    public EventService(ApplicationDbContext _dbContext, IMapper _mapper, ICacheManager _cacheManager)
    {
        this._dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
        this._mapper = _mapper ?? throw new ArgumentNullException(nameof(_mapper));
        this._cacheManager = _cacheManager ?? throw new ArgumentException(nameof(_cacheManager));
    }
    public async Task<ServiceResponse<string>> CreateEvent(CreateEventDto createEventDto)
    {
        ServiceResponse<string> response = new();
        var eventToCreate = _mapper.Map<Event>(createEventDto);
        eventToCreate.CreatedAt = new DateTime();
        eventToCreate.Id = Guid.NewGuid().ToString();
        await _dbContext.Events!.AddAsync(eventToCreate);
        await _dbContext.SaveChangesAsync();
        response.Data = eventToCreate.Id;
        return response;
    }

    public async Task<ServiceResponse<string>> DeleteEvent(string eventId)
    {
        ServiceResponse<string> response = new();
        var eventToDelet = await _dbContext.Events!.FirstOrDefaultAsync(x => x.Id!.Equals(eventId)) ?? throw new CustomException("Not Found", HttpStatusCode.NotFound);
        _dbContext.Events!.Remove(eventToDelet);
        response.Data = eventToDelet.Id;

        return response;
    }

    public async Task<PagedResponse<IEnumerable<EventDto>>> GetAllEvent(PaginatedReq paginatedReq)
    {
        string key = $"EVENTS_{paginatedReq.PageNumber}_{paginatedReq.PageSize}";
        var allEvents = await _cacheManager.GetAsync<PagedResponse<IEnumerable<EventDto>>>(key);
        if (allEvents == null)
        {
            var result = await _dbContext.Events!
                .Skip((paginatedReq.PageNumber - 1) * paginatedReq.PageSize)
                .Take(paginatedReq.PageSize).ToListAsync();

            var dtoResult = result.Select(x => _mapper.Map<EventDto>(x));
            int totalRecords = await _dbContext.Events!.CountAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse(dtoResult, paginatedReq, totalRecords);
            await _cacheManager.SetAsync(key, pagedResponse, TimeSpan.FromMinutes(20));
            return pagedResponse;
        }
        return allEvents;
    }

    public async Task<ServiceResponse<EventDto>> GetEvent(string eventId)
    {
        ServiceResponse<EventDto> response = new();
        var result = await _dbContext.Events!.FirstOrDefaultAsync(x => x.Id!.Equals(eventId)) ?? throw new CustomException("Not Found", HttpStatusCode.NotFound);

        response.Data = _mapper.Map<EventDto>(result);
        return response;
    }
}