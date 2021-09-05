using Microsoft.EntityFrameworkCore;
using WeatherForecast.Core.Models;
using WeatherForecast.Data.Configurations;

namespace WeatherForecast.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Weather> WeatherForecasts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new WeatherForecastConfiguration());
        }
    }
}
