using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Models;

namespace WeatherForecast.Core.Services
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<Weather>> GetAll();
        Task<Weather> GetWeatherForecastById(int id);
        Task<Weather> GetWeatherForecastByDate(DateTime date);
        Task<Weather> CreateWeatherForecast(Weather newWeatherForecast);
        Task UpdateWeatherForecast(Weather forecastToBeUpdated, Weather weatherForecast);
        Task DeleteWeatherForecast(Weather weatherForecast);
    }
}
