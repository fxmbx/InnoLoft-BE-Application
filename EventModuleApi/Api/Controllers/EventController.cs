using EventModuleApi.Core.Contracts;
using EventModuleApi.Infrastructure.Helper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EventModuleApi.Api.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILoggerService _logger;

        public EventController(IEventService _eventService, ILoggerService _logger)
        {
            this._eventService = _eventService ?? throw new ArgumentNullException(nameof(_eventService));
            this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));

        }
        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetById([FromRoute] string eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ExceptionWrapper<EventDto> exceptionWrapper = new(_logger);
            var response = await exceptionWrapper.CallMethodAsync(() =>
            {
                return _eventService.GetEvent(eventId);
            });
            return StatusCode((int)response.StatusCode!, response);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginatedReq paginatedReq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ExceptionWrapper<IEnumerable<EventDto>> exceptionWrapper = new(_logger);
            var response = await exceptionWrapper.CallMethodAsync(() =>
            {
                return _eventService.GetAllEvent(paginatedReq);
            });
            return StatusCode((int)response.StatusCode!, response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEventDto createEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ExceptionWrapper<string> exceptionWrapper = new(_logger);
            var response = await exceptionWrapper.CallMethodAsync(() =>
            {
                return _eventService.CreateEvent(createEventDto);
            });
            return StatusCode((int)response.StatusCode!, response);
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete([FromRoute] string eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ExceptionWrapper<string> exceptionWrapper = new(_logger);
            var response = await exceptionWrapper.CallMethodAsync(() =>
            {
                return _eventService.DeleteEvent(eventId);
            });
            return StatusCode((int)response.StatusCode!, response);
        }
    }
}