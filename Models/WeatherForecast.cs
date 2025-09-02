namespace WebApiCiCd.Models
{
    // Модель данных для прогноза погоды
    public record WeatherForecast(DateTime Date, int TemperatureC, string Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
