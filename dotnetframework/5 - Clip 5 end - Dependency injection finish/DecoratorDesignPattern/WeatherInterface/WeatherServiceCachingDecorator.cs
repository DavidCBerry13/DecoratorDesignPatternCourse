using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class WeatherServiceCachingDecorator : IWeatherService
    {


        public WeatherServiceCachingDecorator(IWeatherService weatherClient, Cache cache)
        {
            _decoratedWeatherClient = weatherClient;
            _cache = cache;
        }
        
        private IWeatherService _decoratedWeatherClient;
        private Cache _cache;

        public CurrentWeather GetCurrentWeather(string location)
        {
            Cache c = new Cache();
            string cacheKey = $"WeatherConditions::{location}";

            
            var currentWeather = _cache[cacheKey] as CurrentWeather;
            if (currentWeather == null)
            {
                currentWeather = _decoratedWeatherClient.GetCurrentWeather(location);                
                _cache.Insert(cacheKey, currentWeather, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
            }
            return currentWeather;
        }



        public LocationForecast GetForecast(string location)
        {
            return _decoratedWeatherClient.GetForecast(location);
        }

    }
}