using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Core;
using WeatherForecast.Core.Models;
using WeatherForecast.Core.Services;

namespace WeatherForecast.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WeatherForecastService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Weather> CreateWeatherForecast(Weather newWeatherForecast)
        {
            newWeatherForecast.TemperatureF = WeatherForecastServiceHelper.ConvertCtoF(newWeatherForecast.TemperatureC);
            await _unitOfWork.WeatherForecasts.AddAsync(newWeatherForecast);
            await _unitOfWork.CommitAsync();
            return newWeatherForecast;
        }

        public async Task DeleteWeatherForecast(Weather weatherForecast)
        {
            _unitOfWork.WeatherForecasts.Remove(weatherForecast);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Weather>> GetAll()
        {
            return await _unitOfWork.WeatherForecasts.GetAllAsync();
        }

        public async Task<Weather> GetWeatherForecastByDate(DateTime date)
        {
            return await _unitOfWork.WeatherForecasts.GetWeatherForecastByDateAsync(date);
        }

        public async Task<Weather> GetWeatherForecastById(int id)
        {
            return await _unitOfWork.WeatherForecasts.GetByIdAsync(id);
        }

        public async Task UpdateWeatherForecast(Weather forecastToBeUpdated, Weather weatherForecast)
        {
            forecastToBeUpdated.TemperatureC = weatherForecast.TemperatureC;
            forecastToBeUpdated.TemperatureF = WeatherForecastServiceHelper.ConvertCtoF(weatherForecast.TemperatureC);
            forecastToBeUpdated.Summary = weatherForecast.Summary;

            await _unitOfWork.CommitAsync();
        }
    }
}
