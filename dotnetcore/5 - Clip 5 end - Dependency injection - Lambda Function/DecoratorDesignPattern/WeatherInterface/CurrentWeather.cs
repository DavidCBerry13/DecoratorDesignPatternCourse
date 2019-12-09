using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.WeatherInterface
{
    public class CurrentWeather
    {

        public bool Success { get; set; }

        public String ErrorMessage { get; set; }

        public LocationData Location { get; set; }

        public DateTime ObservationTime { get; set; }

        public DateTime ObservationTimeUtc { get; set; }

        public WeatherData CurrentConditions { get; set; }

        public class LocationData
        { 
        
            public String Name { get; set; }

            public double Latitude { get; set; }

            public double Longitude { get; set; }
        }


        public class WeatherData
        {
            public String Conditions { get; set; }

            public String ConditionsDescription { get; set; }

            public double Visibility { get; set; }

            public int CloudCover { get; set; }

            public double Temperature { get; set; }

            public double Pressure { get; set; }

            public double Humidity { get; set; }

            public double WindSpeed { get; set; }

            public CompassDirection WindDirection { get; set; }

            public double WindDirectionDegrees { get; set; }

            public double RainfallOneHour { get; set; }

        }


        public DateTime FetchTime { get; set; }

    }



    

}
