using System.Net;
using EventModuleApi.Core.Contracts;
using EventModuleApi.Infrastructure.Helper;
using EventModuleApi.Response;
using Moq;

namespace EventModuleApi.Test.System
{
    public class ExceptionWrapperTests
    {
        [Fact]
        public async Task CallMethodAsync_SuccessfulExecution_ShouldReturnServiceResponse()
        {
            // Arrange
            var expectedResult = new ServiceResponse<int> { Data = 42 };
            var mockLogger = new Mock<ILoggerService>();
            var exceptionWrapper = new ExceptionWrapper<int>(mockLogger.Object);

            // Create a mock repository method that returns the expected result
            Func<Task<ServiceResponse<int>>> method = () => Task.FromResult(expectedResult);

            // Act
            var result = await exceptionWrapper.CallMethodAsync(method);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.False(result.IsError);
            Assert.Equal("Success", result.Message);
        }

        [Fact]
        public async Task CallMethodAsync_CustomException_ShouldHandleAndLogException()
        {
            // Arrange
            var expectedException = new CustomException("Custom Exception Message", HttpStatusCode.Conflict);
            var mockLogger = new Mock<ILoggerService>();
            var exceptionWrapper = new ExceptionWrapper<int>(mockLogger.Object);

            // Create a mock repository method that throws the custom exception
            Func<Task<ServiceResponse<int>>> method = () => throw expectedException;

            // Act
            var result = await exceptionWrapper.CallMethodAsync(method);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(expectedException.GetStatusCode(), result.StatusCode);
            Assert.Equal(expectedException.Message, result.Message);

            // Verify that the logger was called with the correct parameters
            mockLogger.Verify(
                logger => logger.LogWarning(expectedException.Message, It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task CallMethodAsync_GenericException_ShouldHandleAndLogException()
        {
            // Arrange
            var expectedException = new Exception("Generic Exception Message");
            var mockLogger = new Mock<ILoggerService>();
            var exceptionWrapper = new ExceptionWrapper<int>(mockLogger.Object);

            // Create a mock repository method that throws a generic exception
            Func<Task<ServiceResponse<int>>> method = () => throw expectedException;

            // Act
            var result = await exceptionWrapper.CallMethodAsync(method);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);

            // Verify that the logger was called with the correct parameters
            mockLogger.Verify(
                logger => logger.LogError(expectedException.Message, It.IsAny<string>()),
                Times.Once);
        }

    }
}