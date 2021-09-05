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
        private readonly IMapper _mapper;

        public WeatherForecastController(IWeatherForecastService weatherForecastService, IMapper mapper)
        {
            this._weatherForecastService = weatherForecastService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecastResource>>> GetAll()
        {
            var weatherForecasts = await _weatherForecastService.GetAll();
            //var weatherForecastResources = _mapper.Map<IEnumerable<Weather>, IEnumerable<WeatherForecastResource>>(weatherForecasts);

            return Ok(weatherForecasts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecastResource>> GetWeatherForecastById(int id)
        {
            var weatherForecast = await _weatherForecastService.GetWeatherForecastById(id);
            //var weatherForecastResource = _mapper.Map<Weather, WeatherForecastResource>(weatherForecast);

            return Ok(weatherForecast);
        }
        /*
                [HttpGet("{date}"), Name = "GetWeatherForecastByDate"]
                public async Task<ActionResult<WeatherForecastResource>> GetWeatherForecastByDate(DateTime date)
                {
                    var weatherForecast = await _weatherForecastService.GetWeatherForecastByDate(date);
                    var weatherForecastResource = _mapper.Map<Weather, WeatherForecastResource>(weatherForecast);

                    return Ok(weatherForecastResource);
                }
        */
        /*
                [HttpPost]
                public async Task<ActionResult<WeatherForecastResource>> CreateWeatherForecast([FromBody] SaveWeatherForecastResource saveWeatherForecastResource)
                {
                    var validator = new SaveWeatherForecastResourceValidator();
                    var validationResult = await validator.ValidateAsync(saveWeatherForecastResource);

                    if (!validationResult.IsValid)
                        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                    var weatherForecastToCreate = _mapper.Map<SaveWeatherForecastResource, Weather>(saveWeatherForecastResource);

                    var newWeatherForecest = await _weatherForecastService.CreateWeatherForecast(weatherForecastToCreate);

                    var weather = await _weatherForecastService.GetWeatherForecastById(newWeatherForecest.Id);

                    var weatherForecastResource = _mapper.Map<Weather, WeatherForecastResource>(weather);

                    return Ok(weatherForecastResource);
                }
        */
        [HttpPost]
        public async Task<ActionResult<WeatherForecastResource>> CreateWeatherForecast([FromBody] Weather weatherForecast)
        {
            var newWeatherForecest = await _weatherForecastService.CreateWeatherForecast(weatherForecast);

            var weather = await _weatherForecastService.GetWeatherForecastById(newWeatherForecest.Id);

            return Ok(weather);
        }
        /*
                [HttpPut("{id}")]
                public async Task<ActionResult<WeatherForecastResource>> UpdateWeatherForecast(int id, [FromBody] SaveWeatherForecastResource saveWeatherForecastResource)
                {
                    var validator = new SaveWeatherForecastResourceValidator();
                    var validationResult = await validator.ValidateAsync(saveWeatherForecastResource);

                    var requestIsInvalid = id == 0 || !validationResult.IsValid;

                    if (requestIsInvalid)
                        return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

                    var weatherForecastToBeUpdate = await _weatherForecastService.GetWeatherForecastById(id);

                    if (weatherForecastToBeUpdate == null)
                        return NotFound();

                    var weatherForecast = _mapper.Map<SaveWeatherForecastResource, Weather>(saveWeatherForecastResource);

                    await _weatherForecastService.UpdateWeatherForecast(weatherForecastToBeUpdate, weatherForecast);

                    var updatedWeatherForecast = await _weatherForecastService.GetWeatherForecastById(id);
                    var updatedWeatherForecastResource = _mapper.Map<Weather, WeatherForecastResource>(updatedWeatherForecast);

                    return Ok(updatedWeatherForecastResource);
                }
        */
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
