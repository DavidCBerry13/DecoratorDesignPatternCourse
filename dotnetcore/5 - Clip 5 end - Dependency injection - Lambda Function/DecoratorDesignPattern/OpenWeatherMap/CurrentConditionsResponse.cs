using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecoratorDesignPattern.OpenWeatherMap
{



    public class CurrentConditionsResponse
    {
        /// <summary>
        /// A description of any error that occured
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coordinates Coordintates { get; set; }

        [JsonProperty("dt")]
        public int ObservationTime { get; set; }

        [JsonProperty("weather")]
        public ObservedConditions[] ObservedConditions { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("rain")]
        public RainData Rain { get; set; }

        [JsonProperty("main")]
        public ObservationData ObservationData { get; set; }

        [JsonProperty("wind")]
        public WindData WindData { get; set; }

        [JsonProperty("sys")]
        public LocationDetails LocationDetails { get; set; }

        [JsonProperty("timezone")]
        public int TimezoneOffset { get; set; }
    }


    public class Coordinates
    {
        [JsonProperty("lon")]
        public float Longitude { get; set; }

        [JsonProperty("lat")]
        public float Latitude { get; set; }
    }

    public class ObservationData
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("temp_min")]
        public float MinTemperature { get; set; }

        [JsonProperty("temp_max")]
        public float MaxTemperature { get; set; }
    }

    public class WindData
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("deg")]
        public int Degrees { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public int CloudCover { get; set; }
    }

    public class RainData
    {
        [JsonProperty("1h")]
        public double RainfallOneHour { get; set; }
    }

    public class LocationDetails
    {
        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }

    public class ObservedConditions
    {
        [JsonProperty("id")]
        public int DescriptionId { get; set; }

        [JsonProperty("main")]
        public string Conditions { get; set; }

        [JsonProperty("description")]
        public string ConditionsDetail { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

}