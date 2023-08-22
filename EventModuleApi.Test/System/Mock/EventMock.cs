using AutoFixture;
using EventModuleApi.Dto;

namespace EventModuleApi.Test.System.Mock
{
    public class EventMock
    {
        private readonly IFixture _fixture;

        public EventMock()
        {
            _fixture = new Fixture();

        }
        public List<EventDto> GetModuleMockData()
        {
            return new List<EventDto>()
            {
                new EventDto()
                {
                    Id= _fixture.Create<string>(),
                    TimeZone = _fixture.Create<string>(),
                    StartDate = _fixture.Create<DateTime>(),
                    EndDate = _fixture.Create<DateTime>(),
                    EventDescription =  _fixture.Create<string>(),
                    EventTitle =  _fixture.Create<string>(),
                    EventType =  _fixture.Create<string>()

                },
                new EventDto()
                {
                    Id= _fixture.Create<string>(),
                    TimeZone = _fixture.Create<string>(),
                    StartDate = _fixture.Create<DateTime>(),
                    EndDate = _fixture.Create<DateTime>(),
                    EventDescription =  _fixture.Create<string>(),
                    EventTitle =  _fixture.Create<string>(),
                    EventType =  _fixture.Create<string>()

                },
                new EventDto()
                {
                    Id= _fixture.Create<string>(),
                    TimeZone = _fixture.Create<string>(),
                    StartDate = _fixture.Create<DateTime>(),
                    EndDate = _fixture.Create<DateTime>(),
                    EventDescription =  _fixture.Create<string>(),
                    EventTitle =  _fixture.Create<string>(),
                    EventType =  _fixture.Create<string>()

                },
            };

        }
    }
}