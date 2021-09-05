using System;
using System.Threading.Tasks;
using WeatherForecast.Core.Models;

namespace WeatherForecast.Core.Repositories
{
    public interface IWeatherForecastRepository : IRepository<Weather>
    {
        Task<Weather> GetWeatherForecastByDateAsync(DateTime date);
    }
}
