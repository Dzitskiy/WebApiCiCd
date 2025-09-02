using WebApiCiCd.Models;

namespace WebApiCiCd.Services
{
    // Реализация сервиса
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public IEnumerable<WeatherForecast> GetForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
                .ToArray();
        }
    }
}
