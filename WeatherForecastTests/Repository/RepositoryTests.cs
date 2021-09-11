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
    [TestFixture]
    class RepositoryTests
    {
        private ApplicationDbContext _context;
        private List<Weather> _forecasts;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: "weatherforecasts")
                  .Options;

            _context = new ApplicationDbContext(options);
            _forecasts = new List<Weather>
            {
                new Weather
                {
                    Id = 1,
                    Date = DateTime.MinValue,
                    TemperatureC = 23,
                    Summary = "Cloudy"
                },
                new Weather
                {
                    Id = 2,
                    Date = DateTime.MinValue,
                    TemperatureC = 33,
                    Summary = "Hot"
                },
                new Weather
                {
                    Id = 3,
                    Date = DateTime.MinValue,
                    TemperatureC = 63,
                    Summary = "Fryer"
                },
                new Weather
                {
                    Id = 4,
                    Date = DateTime.Today,
                    TemperatureC = 13,
                    Summary = "Cold"
                }
            };

            _context.AddRange(_forecasts);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public void Get_all_weather_forecasts()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            // Act
            var weatherForecasts = repository.GetAllAsync();

            // Assert
            Assert.AreEqual(_forecasts.Count, weatherForecasts.Result.Count());

        }
        [Test]
        public void Get_weather_forecast_by_id()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            // Act
            var weatherForecast = repository.GetByIdAsync(1);

            // Assert
            Assert.AreEqual(1, weatherForecast.Result.Id);
            Assert.AreEqual(DateTime.MinValue, weatherForecast.Result.Date);
            Assert.AreEqual(23, weatherForecast.Result.TemperatureC);
            Assert.AreEqual("Cloudy", weatherForecast.Result.Summary);

        }

        [Test]
        public void Get_weather_forecast_by_date()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            // Act
            var weatherForecast = repository.GetWeatherForecastByDateAsync(DateTime.Today);

            // Assert
            Assert.AreEqual(4, weatherForecast.Result.Id);
            Assert.AreEqual(DateTime.Today, weatherForecast.Result.Date);
            Assert.AreEqual(13, weatherForecast.Result.TemperatureC);
            Assert.AreEqual("Cold", weatherForecast.Result.Summary);
        }

        [Test]
        public async Task Add_weather_forecast_async()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            Weather forecastToAdd = new Weather
            {
                Id = 5,
                Date = DateTime.MaxValue,
                TemperatureC = 7,
                Summary = "Fine"
            };

            // Act
            await repository.AddAsync(forecastToAdd);
            _context.SaveChanges();

            var newForecast = repository.GetByIdAsync(5);
            var forecasts = repository.GetAllAsync();

            // Assert
            Assert.AreEqual(forecastToAdd.Id, newForecast.Result.Id);
            Assert.AreEqual(_forecasts.Count + 1, forecasts.Result.Count());
        }

        [Test]
        public async Task Add_weather_forecasts_async()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            List<Weather> forecastsToAdd = new List<Weather>();

            forecastsToAdd.Add(
                new Weather
                {
                    Id = 5,
                    Date = DateTime.MaxValue,
                    TemperatureC = 7,
                    Summary = "Fine"
                });
            forecastsToAdd.Add(
                 new Weather
                 {
                     Id = 6,
                     Date = DateTime.MinValue,
                     TemperatureC = 12,
                     Summary = "RRR"
                 });

            // Act
            await repository.AddRangeAsync(forecastsToAdd);
            _context.SaveChanges();

            var forecasts = repository.GetAllAsync();

            // Assert
            Assert.AreEqual(_forecasts.Count + forecastsToAdd.Count, forecasts.Result.Count());
            Assert.AreEqual(forecastsToAdd[0].Id, repository.GetByIdAsync(5).Result.Id);
            Assert.AreEqual(forecastsToAdd[1].Id, repository.GetByIdAsync(6).Result.Id);
        }

        [Test]
        public void Remove_weather_forecast()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            // Act
            repository.Remove(_forecasts[2]);
            _context.SaveChanges();

            var forecasts = repository.GetAllAsync();

            // Assert
            Assert.AreEqual(_forecasts.Count - 1, forecasts.Result.Count());
        }

        [Test]
        public void Remove_weather_forecasts()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            // Act
            repository.RemoveRange(_forecasts);
            _context.SaveChanges();

            var forecasts = repository.GetAllAsync();

            // Assert
            Assert.AreEqual(0, forecasts.Result.Count());
        }

        [Test]
        public void Find_weather_forecast()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);
            DateTime lookinForDate = DateTime.MinValue;

            // Act
            var forecasts = repository.Find(wf => wf.Date == lookinForDate);

            // Assert
            foreach (Weather forecast in forecasts)
            {
                Assert.AreEqual(lookinForDate, forecast.Date);
            }
        }

        [Test]
        public void Single_or_Default()
        {
            // Arrange
            WeatherForecastRepository repository = new WeatherForecastRepository(_context);

            // Act
            var forecast = repository.SingleOrDefaultAsync(wf => wf.Summary.Equals("Cold"));

            // Assert
            Assert.AreEqual("Cold", forecast.Result.Summary);
            Assert.AreEqual(4, forecast.Result.Id);
        }
    }
}

