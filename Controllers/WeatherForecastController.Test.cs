using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiCiCd.Controllers;
using WebApiCiCd.Models;
using WebApiCiCd.Services;
using Xunit;

namespace WebApi.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<IWeatherService> _mockWeatherService;
        private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            _mockWeatherService = new Mock<IWeatherService>();
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(_mockWeatherService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast(DateTime.Now, 25, "Warm"),
                new WeatherForecast(DateTime.Now.AddDays(1), 30, "Hot")
            };

            _mockWeatherService.Setup(service => service.GetForecast())
                .Returns(forecasts);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<WeatherForecast>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public void Get_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast(DateTime.Now, 25, "Warm"),
                new WeatherForecast(DateTime.Now.AddDays(1), 30, "Hot")
            };

            _mockWeatherService.Setup(service => service.GetForecast())
                .Returns(forecasts);

            // Act
            var result = _controller.Get(0);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<WeatherForecast>(okResult.Value);
            Assert.Equal("Warm", returnValue.Summary);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast(DateTime.Now, 25, "Warm"),
                new WeatherForecast(DateTime.Now.AddDays(1), 30, "Hot")
            };

            _mockWeatherService.Setup(service => service.GetForecast())
                .Returns(forecasts);

            // Act
            var result = _controller.Get(10);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}