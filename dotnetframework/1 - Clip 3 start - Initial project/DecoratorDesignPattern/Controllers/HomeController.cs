using DecoratorDesignPattern.OpenWeatherMap;
using DecoratorDesignPattern.WeatherInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.Web.Configuration;

namespace DecoratorDesignPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly Cache _cache;


        public HomeController(Cache cache)
        {
            _cache = cache;

            String apiKey = WebConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
            _weatherService = new WeatherService(apiKey);
        }

        public ActionResult Index(string location = "Chicago")
        {
            CurrentWeather conditions = _weatherService.GetCurrentWeather(location);
            return View(conditions);
        }

        public ActionResult Forecast(string location = "Chicago")
        {
            LocationForecast forecast = _weatherService.GetForecast(location);
            return View(forecast);
        }

        public ActionResult ApiKey()
        {
            return View();
        }
    }
}