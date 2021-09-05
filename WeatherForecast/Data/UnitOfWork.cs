using System.Threading.Tasks;
using WeatherForecast.Core;
using WeatherForecast.Core.Repositories;
using WeatherForecast.Data.Repositories;

namespace WeatherForecast.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly WeatherForecastRepository _weatherForecastRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }
        public IWeatherForecastRepository WeatherForecasts => _weatherForecastRepository ?? new WeatherForecastRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
