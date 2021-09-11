using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Services
{
    public class WeatherForecastServiceHelper
    {
        public static int ConvertCtoF(int temperatureC)
        {
            return 32 + (int)(temperatureC / 0.5556);
        }
    }
}
