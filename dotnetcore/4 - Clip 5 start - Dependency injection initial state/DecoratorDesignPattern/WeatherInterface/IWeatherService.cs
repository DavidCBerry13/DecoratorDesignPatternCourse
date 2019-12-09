using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.WeatherInterface
{
    public interface IWeatherService
    {

        CurrentWeather GetCurrentWeather(String location);


        LocationForecast GetForecast(String location);


    }
}
