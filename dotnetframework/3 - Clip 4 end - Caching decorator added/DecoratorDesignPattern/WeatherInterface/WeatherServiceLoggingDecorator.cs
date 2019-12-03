using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class WeatherServiceLoggingDecorator : IWeatherService
    {
        public WeatherServiceLoggingDecorator(IWeatherService weatherService)
        {
            _innerWeatherService = weatherService;
        }


        private IWeatherService _innerWeatherService;

        public CurrentWeather GetCurrentWeather(string location)
        {
            var sw = Stopwatch.StartNew();
            var currentWeather = _innerWeatherService.GetCurrentWeather(location);
            sw.Stop();
            var elapsedMillis = sw.ElapsedMilliseconds;
            Log.Logger.Warning("Retrieved weather data for {location} - Elapsed ms: {} {@currentWeather}", location, elapsedMillis, currentWeather);

            return currentWeather;
        }

        public LocationForecast GetForecast(string location)
        {
            return _innerWeatherService.GetForecast(location);
        }
    }
}