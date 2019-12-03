using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class WeatherServiceCachingDecorator : IWeatherService
    {


        public WeatherServiceCachingDecorator(IWeatherService weatherClient, IMemoryCache cache)
        {
            _decoratedWeatherClient = weatherClient;
            _cache = cache;
        }

        private IMemoryCache _cache;
        private IWeatherService _decoratedWeatherClient;


        public CurrentWeather GetCurrentWeather(string location)
        {
            string cacheKey = $"WeatherConditions::{location}";
            if ( _cache.TryGetValue<CurrentWeather>(cacheKey, out var currentWeather))
            {
                return currentWeather;
            }
            else
            {
                var currentConditions = _decoratedWeatherClient.GetCurrentWeather(location);
                _cache.Set<CurrentWeather>(cacheKey, currentConditions, TimeSpan.FromMinutes(30));
                return currentConditions;
            }
            
        }



        public LocationForecast GetForecast(string location)
        {
            return _decoratedWeatherClient.GetForecast(location);
        }

    }
}
