using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Resources;

namespace WeatherForecast.Validators
{
    public class SaveWeatherForecastResourceValidator : AbstractValidator<SaveWeatherForecastResource>
    {
        public SaveWeatherForecastResourceValidator()
        {
            RuleFor(a => a.Date)
                .NotEmpty();
            RuleFor(a => a.Summary)
                .NotEmpty();
        }
    }
}
