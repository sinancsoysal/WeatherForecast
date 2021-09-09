using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Core.Models;
using WeatherForecast.Data;
using WeatherForecast.Data.Repositories;

namespace WeatherForecastTests.Repository
{
    class RepositoryTests
    {
        [Test]
        public void WeatherForecastRepository_GetWeatherForecastById_Valid_Forecast_Id()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "weatherforecasts")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.WeatherForecasts.Add(new Weather
                {
                    Id = 1,
                    Date = DateTime.Now,
                    TemperatureC = 23,
                    Summary = "Cloudy"
                });
                context.WeatherForecasts.Add(new Weather
                {
                    Id = 2,
                    Date = DateTime.Now,
                    TemperatureC = 33,
                    Summary = "Hot"
                });
                context.WeatherForecasts.Add(new Weather
                {
                    Id = 3,
                    Date = DateTime.Now,
                    TemperatureC = 63,
                    Summary = "Fryer"
                });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                WeatherForecastRepository repository = new WeatherForecastRepository(context);
                var weatherForecast = repository.GetByIdAsync(1);

                Assert.AreEqual(1, weatherForecast.Result.Id);
            }
        }
    }
}
