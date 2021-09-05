using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Models;

namespace WeatherForecast.Core.Repositories
{
    public interface IWeatherForecastRepository : IRepository<Weather>
    {
        Task<Weather> GetWeatherForecastByDateAsync(DateTime date);
    }
}
