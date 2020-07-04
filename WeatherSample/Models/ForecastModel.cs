using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherSample.Models
{
    public class ForecastModel
    {
        public class City
        {
            [JsonProperty("city_name")] public string CityName { get; set; } = string.Empty;
            [JsonProperty("forecasts")] public List<Forecast> Forecasts { get; set; } = new List<Forecast>();
        }

        public class Forecast
        {
            [JsonProperty("description")] public string Description { get; set; } = string.Empty;
            [JsonProperty("wind_speed")] public double WindSpeed { get; set; }
            [JsonProperty("humidity")] public long Humidity { get; set; }
            [JsonProperty("pressure")] public long Pressure { get; set; }
            [JsonProperty("temp_min")] public double TempMin { get; set; }
            [JsonProperty("temp_max")] public double TempMax { get; set; }
            [JsonProperty("feels_like")] public double FeelsLike { get; set; }
            [JsonProperty("temp")] public double Temp { get; set; }
            [JsonProperty("local_time")] public string LocalTime { get; set; } = string.Empty;
        }
    }
}