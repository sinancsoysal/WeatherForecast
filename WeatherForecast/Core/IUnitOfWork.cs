using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Repositories;

namespace WeatherForecast.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IWeatherForecastRepository WeatherForecasts { get; }
        Task<int> CommitAsync();
    }
}
