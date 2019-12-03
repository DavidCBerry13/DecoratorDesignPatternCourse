using DecoratorDesignPattern.WeatherInterface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DecoratorDesignPattern.OpenWeatherMap
{
    public class WeatherService : IWeatherService
    {

        private RestClient _apiClient;
        private string _apiKey;

        public WeatherService(String apiKey)
        {
            _apiKey = apiKey;
            _apiClient = new RestClient("http://api.openweathermap.org");
        }


        public CurrentWeather GetCurrentWeather(string location)
        {
            var request = new RestRequest("data/2.5/weather");
            request.AddParameter("q", location); 
            request.AddParameter("units", "imperial");   // Change to metric for metric units.  Also update MapResponse below
            request.AddParameter("appid", _apiKey);

            var response = _apiClient.Get(request);

            if (response.IsSuccessful)
            {
                var currentConditions = JsonConvert.DeserializeObject<CurrentConditionsResponse>(response.Content);
                return MapCurrentConditionsResponse(currentConditions);
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new CurrentWeather() { Success = false, ErrorMessage = $"Weather data for {location} could not be found" };
                    case HttpStatusCode.BadRequest:
                        return new CurrentWeather() { Success = false, ErrorMessage = "Bad request sent to the server.  Make sure you have an API key, otherwise debug through the code to see what went wrong" };
                    default:
                        return new CurrentWeather() { Success = false, ErrorMessage = "Error calling OpenWeatherMap API.  Debug through the code to see what went wrong" };
                }
            }
        }

        private CurrentWeather MapCurrentConditionsResponse(CurrentConditionsResponse openWeatherApiResponse)
        {
            var currentConditions = new CurrentWeather()
            {
                Success = true,
                ErrorMessage = String.Empty,
                Location = new CurrentWeather.LocationData()
                {
                    Name = openWeatherApiResponse.Name,
                    Latitude = openWeatherApiResponse.Coordintates.Latitude,
                    Longitude = openWeatherApiResponse.Coordintates.Longitude
                },
                ObservationTime = DateTimeOffset.FromUnixTimeSeconds(openWeatherApiResponse.ObservationTime + openWeatherApiResponse.TimezoneOffset).DateTime,
                ObservationTimeUtc = DateTimeOffset.FromUnixTimeSeconds(openWeatherApiResponse.ObservationTime).DateTime,
                CurrentConditions = new CurrentWeather.WeatherData()
                {
                    Conditions = openWeatherApiResponse.ObservedConditions.FirstOrDefault()?.Conditions,
                    ConditionsDescription = openWeatherApiResponse.ObservedConditions.FirstOrDefault()?.ConditionsDetail,
                    Visibility = openWeatherApiResponse.Visibility / 1609.0,  // Visibility always comes back in meters, even when imperial requested
                    CloudCover = openWeatherApiResponse.Clouds.CloudCover,
                    Temperature = openWeatherApiResponse.ObservationData.Temperature,
                    Humidity = openWeatherApiResponse.ObservationData.Humidity,
                    Pressure = openWeatherApiResponse.ObservationData.Pressure * 0.0295301,  // Pressure always comes back in millibars, even when imperial requested
                    WindSpeed = openWeatherApiResponse.WindData.Speed,
                    WindDirection = CompassDirection.GetDirection(openWeatherApiResponse.WindData.Degrees),
                    WindDirectionDegrees = openWeatherApiResponse.WindData.Degrees,
                    RainfallOneHour = (openWeatherApiResponse.Rain?.RainfallOneHour ?? 0.0) * 0.03937008
                },
                FetchTime = DateTime.Now
            };

            return currentConditions;
        }


        public LocationForecast GetForecast(string location)
        {
            var request = new RestRequest("data/2.5/forecast");
            request.AddParameter("q", location);
            request.AddParameter("units", "imperial");   // Change to metric for metric units.  Also update MapResponse below
            request.AddParameter("appid", _apiKey);

            var response = _apiClient.Get(request);

            if (response.IsSuccessful)
            {
                var currentConditions = JsonConvert.DeserializeObject<ForecastResponse>(response.Content);
                return MapForecastResponse(currentConditions);
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new LocationForecast() { Success = false, ErrorMessage = $"Forecast data for {location} could not be found" };
                    case HttpStatusCode.BadRequest:
                        return new LocationForecast() { Success = false, ErrorMessage = "Bad request sent to the server.  Make sure you have an API key, otherwise debug through the code to see what went wrong" };
                    default:
                        return new LocationForecast() { Success = false, ErrorMessage = "Error calling OpenWeatherMap API.  Debug through the code to see what went wrong" };
                }
            }
        }


        private LocationForecast MapForecastResponse(ForecastResponse openWeatherApiResponse)
        {
            var locationForecast = new LocationForecast()
            {
                Success = true,
                ErrorMessage = String.Empty,
                Location = new ForecastLocation()
                {
                    Name = openWeatherApiResponse.Location.Name,
                    Latitude = openWeatherApiResponse.Location.Coordinates.Latitude,
                    Longitude = openWeatherApiResponse.Location.Coordinates.Longitude
                },                
                FetchTime = DateTime.Now
            };

            foreach (var openWeatherForecast in openWeatherApiResponse.ForecastPoints)
            {
                WeatherForecast forecast = new WeatherForecast()
                {
                    Conditions = openWeatherForecast.Conditions.FirstOrDefault()?.main,
                    ConditionsDescription = openWeatherForecast.Conditions.FirstOrDefault()?.description,
                    Temperature = openWeatherForecast.WeatherData.Temperature,
                    Humidity = openWeatherForecast.WeatherData.Humidity,
                    Pressure = openWeatherForecast.WeatherData.pressure * 0.0295301,  // Pressure always comes back in millibars, even when imperial requested,
                    ForecastTime = DateTimeOffset.FromUnixTimeSeconds(openWeatherForecast.Date + openWeatherApiResponse.Location.TimezoneOffset).DateTime,
                    CloudCover = openWeatherForecast.Clouds.CloudCover,
                    WindSpeed = openWeatherForecast.Wind.WindSpeed,
                    WindDirectionDegrees = openWeatherForecast.Wind.WindDirectionDegrees,
                    WindDirection = CompassDirection.GetDirection(openWeatherForecast.Wind.WindDirectionDegrees),
                    ExpectedRainfall = (openWeatherForecast.Rain?.RainfallThreeHours ?? 0.0) * 0.03937008,
                    ExpectedSnowfall = (openWeatherForecast.Snow?.SnowfallThreeHours ?? 0.0) * 0.03937008
                };
                locationForecast.Forecasts.Add(forecast);
            }

            return locationForecast;
        }


    }
}
