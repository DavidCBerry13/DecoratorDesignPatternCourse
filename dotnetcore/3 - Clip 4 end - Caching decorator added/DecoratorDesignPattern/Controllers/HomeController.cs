using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DecoratorDesignPattern.Models;
using DecoratorDesignPattern.OpenWeatherMap;
using DecoratorDesignPattern.WeatherInterface;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorDesignPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILoggerFactory loggerFactory, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<HomeController>();
            _memoryCache = memoryCache;

            String apiKey = configuration.GetValue<String>("AppSettings:OpenWeatherMapApiKey");

            IWeatherService innerService = new WeatherService(apiKey);
            IWeatherService withLoggingDecorator = new WeatherServiceLoggingDecorator(innerService, _loggerFactory.CreateLogger<WeatherServiceLoggingDecorator>());
            IWeatherService withCachingDecorator = new WeatherServiceCachingDecorator(withLoggingDecorator, _memoryCache);
            _weatherService = withCachingDecorator;
        }


        public IActionResult Index(string location = "Chicago")
        {
            CurrentWeather conditions = _weatherService.GetCurrentWeather(location);
            return View(conditions);
        }



        public IActionResult Forecast(string location = "Chicago")
        {
            LocationForecast forecast = _weatherService.GetForecast(location);
            return View(forecast);
        }

        public IActionResult ApiKey()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
