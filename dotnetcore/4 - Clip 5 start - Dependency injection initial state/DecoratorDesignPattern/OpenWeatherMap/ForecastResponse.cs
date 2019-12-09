using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public class ForecastResponse
    {
        public string cod { get; set; }
        public float message { get; set; }

        [JsonProperty("list")]
        public OpenWeatherForecastData[] ForecastPoints { get; set; }

        [JsonProperty("city")]
        public OpenWeatherForecastLocation Location { get; set; }
    }


    public class OpenWeatherForecastData
    {
        [JsonProperty("dt")]
        public int Date { get; set; }

        [JsonProperty("main")]
        public OpenWeatherForecastWeatherData WeatherData { get; set; }

        [JsonProperty("weather")]
        public OpenWeatherForecastConditions[] Conditions { get; set; }

        [JsonProperty("clouds")]
        public OpenWeatherForecastClouds Clouds { get; set; }

        [JsonProperty("wind")]
        public OpenWeatherForecastWind Wind { get; set; }
        public string dt_txt { get; set; }

        [JsonProperty("rain")]
        public OpenWeatherForecastRain Rain { get; set; }

        [JsonProperty("snow")]
        public OpenWeatherForecastSnow Snow { get; set; }
    }


    public class OpenWeatherForecastLocation
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public OpenWeatherForecastCoordinates Coordinates { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("timezone")]
        public int TimezoneOffset { get; set; }
    }

    public class OpenWeatherForecastCoordinates
    {
        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lon")]
        public float Longitude { get; set; }
    }



    public class OpenWeatherForecastWeatherData
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }

        [JsonProperty("Pressure")]
        public float pressure { get; set; }
        public float sea_level { get; set; }
        public float grnd_level { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class OpenWeatherForecastClouds
    {
        [JsonProperty("all")]
        public int CloudCover { get; set; }
    }

    public class OpenWeatherForecastWind
    {
        [JsonProperty("speed")]
        public float WindSpeed { get; set; }

        [JsonProperty("deg")]
        public int WindDirectionDegrees { get; set; }
    }


    public class OpenWeatherForecastRain
    {
        [JsonProperty("3h")]
        public float RainfallThreeHours { get; set; }
    }

    public class OpenWeatherForecastSnow
    {
        [JsonProperty("3h")]
        public float SnowfallThreeHours { get; set; }
    }

    public class OpenWeatherForecastConditions
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}














