using WebApiCiCd.Models;

namespace WebApiCiCd.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetForecast();
    }
}
