using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Resources;
using WeatherForecast.Core.Models;

namespace WeatherForecast.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // domain to resource
            CreateMap<Weather, WeatherForecastResource>();

            // resource to domain
            CreateMap<WeatherForecastResource, Weather>();
        }
    }
}
