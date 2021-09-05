using System;
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
