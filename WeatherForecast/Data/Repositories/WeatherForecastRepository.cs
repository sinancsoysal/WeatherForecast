using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WeatherForecast.Core.Models;
using WeatherForecast.Core.Repositories;

namespace WeatherForecast.Data.Repositories
{
    public class WeatherForecastRepository : Repository<Weather>, IWeatherForecastRepository
    {
        public WeatherForecastRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }
        public async Task<Weather> GetWeatherForecastByDateAsync(DateTime date)
        {
            return await applicationDbContext.WeatherForecasts
                .SingleOrDefaultAsync(wf => wf.Date == date);
        }
        private ApplicationDbContext applicationDbContext
        {
            get { return Context; }
        }
    }
}
