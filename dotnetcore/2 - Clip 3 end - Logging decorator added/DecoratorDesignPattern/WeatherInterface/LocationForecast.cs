using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class LocationForecast
    {
        public LocationForecast()
        {
            Forecasts = new List<WeatherForecast>();
        }

        public bool Success { get; set; }

        public String ErrorMessage { get; set; }

        public ForecastLocation Location { get; set; }

        public DateTime FetchTime { get; set; }

        public List<WeatherForecast> Forecasts { get; private set; }
    }



    public class ForecastLocation
    {

        public String Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }


    public class WeatherForecast
    {
        public DateTime ForecastTime { get; set; }

        public DateTime ForecastTimeUtc { get; set; }

        public String Conditions { get; set; }

        public String ConditionsDescription { get; set; }


        public int CloudCover { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double Humidity { get; set; }

        public double WindSpeed { get; set; }

        public CompassDirection WindDirection { get; set; }

        public double WindDirectionDegrees { get; set; }

        public double ExpectedRainfall { get; set; }

        public double ExpectedSnowfall { get; set; }

    }


}
