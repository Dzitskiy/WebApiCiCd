using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiCiCd;
using WebApiCiCd.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
object value = builder.Services.AddSwaggerGen();

// Добавление Entity Framework In-Memory для демонстрации
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TestDB"));

// Добавление кастомного сервиса
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

// Настройка конвейера HTTP запросов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Мидлварь для обработки исключений
app.UseExceptionHandler("/error");

// Мидлварь для health checks
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", timestamp = DateTime.UtcNow }));
app.MapGet("/info", () => Results.Ok(new
{
    version = "1.0.0",
    description = "Weather Forecast API",
    environment = app.Environment.EnvironmentName
}));

app.Run();




