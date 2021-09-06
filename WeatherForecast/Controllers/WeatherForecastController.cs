using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Core.Models;
using WeatherForecast.Core.Services;
using WeatherForecast.Resources;
using WeatherForecast.Validators;

// i'm trying to figure out how to use mappers to serve information
// this controller violates some principles & its a little messy.d
// ill fix it when im done with the mappers

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            this._weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecastResource>>> GetAll()
        {
            var weatherForecasts = await _weatherForecastService.GetAll();

            return Ok(weatherForecasts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecastResource>> GetWeatherForecastById(int id)
        {
            var weatherForecast = await _weatherForecastService.GetWeatherForecastById(id);

            return Ok(weatherForecast);
        }
 
        [HttpPost]
        public async Task<ActionResult<WeatherForecastResource>> CreateWeatherForecast([FromBody] Weather weatherForecast)
        {
            var newWeatherForecest = await _weatherForecastService.CreateWeatherForecast(weatherForecast);

            var weather = await _weatherForecastService.GetWeatherForecastById(newWeatherForecest.Id);

            return Ok(weather);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WeatherForecastResource>> UpdateWeatherForecast(int id, [FromBody] Weather weatherForecast)
        {
            var weatherForecastToBeUpdate = await _weatherForecastService.GetWeatherForecastById(id);

            if (weatherForecastToBeUpdate == null) return NotFound();

            await _weatherForecastService.UpdateWeatherForecast(weatherForecastToBeUpdate, weatherForecast);

            var updatedWeatherForecast = await _weatherForecastService.GetWeatherForecastById(id);

            return Ok(updatedWeatherForecast);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeather(int id)
        {
            var weatherForecast = await _weatherForecastService.GetWeatherForecastById(id);

            if (weatherForecast == null)
                return NotFound();

            await _weatherForecastService.DeleteWeatherForecast(weatherForecast);

            return Ok();
        }
    }
}
