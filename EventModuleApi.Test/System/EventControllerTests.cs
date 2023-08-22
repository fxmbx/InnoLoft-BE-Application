using Moq;
using AutoFixture;
using EventModuleApi.Test.System.Mock;
using EventModuleApi.Dto;
using System.Net;
using EventModuleApi.Response;
using EventModuleApi.Request;
using EventModuleApi.Core.Contracts;

namespace EventModuleApi.Test.System
{
    public class EventControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEventService> _eventSrvMock;
        private readonly EventMock moqDat = new();

        public EventControllerTests()
        {
            _fixture = new Fixture();
            _eventSrvMock = new Mock<IEventService>();
        }

        [Fact]
        public async Task CreateEvent_ShouldReturnCreated_WhenInputValid()
        {
            //Arrange
            _eventSrvMock.Setup(x => x.CreateEvent(It.IsAny<CreateEventDto>())).ReturnsAsync(
                 new ServiceResponse<string>()
                 {
                     Data = _fixture.Create<string>(),
                     Description = _fixture.Create<string>(),
                     StatusCode = HttpStatusCode.Created
                 });
            //Act
            var result = await _eventSrvMock.Object.CreateEvent(It.IsAny<CreateEventDto>());

            //Assert
            Assert.NotNull(result.Data);
            Assert.False(result.IsError);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task CreateGroup_ShouldReturnThrowError_WhenEventExist()
        {
            //Arrange
            _eventSrvMock.Setup(x => x.CreateEvent(It.IsAny<CreateEventDto>())).Throws(new Exception());

            //Assert
            await Assert.ThrowsAsync<Exception>(() => _eventSrvMock.Object.CreateEvent(It.IsAny<CreateEventDto>()));
        }

        [Fact]
        public async Task GetEvent_ShouldReturnOk_WhenDataFound()
        {
            //Arrange
            _eventSrvMock.Setup(x => x.GetEvent(It.IsAny<string>())).ReturnsAsync(
                 new ServiceResponse<EventDto>()
                 {
                     Data = _fixture.Create<EventDto>(),
                     Description = _fixture.Create<string>(),
                     StatusCode = HttpStatusCode.OK

                 });
            //Act
            var result = await _eventSrvMock.Object.GetEvent(It.IsAny<string>());

            //Assert
            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<EventDto>(result.Data);
            Assert.False(result.IsError);
            Assert.Equal(HttpStatusCode.OK, result?.StatusCode);
        }

        [Fact]
        public async Task GetEvents_ShouldReturnOk_WhenDataFound()
        {
            //Arrange
            _eventSrvMock.Setup(x => x.GetAllEvent(It.IsAny<PaginatedReq>())).ReturnsAsync(
                 new PagedResponse<IEnumerable<EventDto>>()
                 {
                     Data = _fixture.Create<IEnumerable<EventDto>>(),
                     Description = _fixture.Create<string>(),
                     StatusCode = HttpStatusCode.OK

                 });
            //Act
            var result = await _eventSrvMock.Object.GetAllEvent(It.IsAny<PaginatedReq>());

            //Assert
            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<IEnumerable<EventDto>>(result.Data);
            Assert.False(result.IsError);
            Assert.Equal(HttpStatusCode.OK, result?.StatusCode);
        }

        [Fact]
        public async Task RemoveEvents_ShouldReturnOk_WhenDataDeleted()
        {
            //Arrange
            _eventSrvMock.Setup(x => x.DeleteEvent(It.IsAny<string>())).ReturnsAsync(
                 new ServiceResponse<string>()
                 {
                     Data = _fixture.Create<string>(),
                     StatusCode = HttpStatusCode.OK

                 });
            //Act
            var result = await _eventSrvMock.Object.DeleteEvent(It.IsAny<string>());

            //Assert
            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<string>(result.Data);
            Assert.False(result.IsError);
            Assert.Equal(HttpStatusCode.OK, result?.StatusCode);
        }


    }
}
