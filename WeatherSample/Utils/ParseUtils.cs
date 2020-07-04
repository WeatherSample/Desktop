using System;
using System.Collections.Generic;
using System.Linq;
using WeatherSample.Models;

namespace WeatherSample.Utils
{
    /// <summary>
    /// Class stores utils methods.
    /// </summary>
    public static class ParseUtils
    {
        /// <summary>
        /// Do parse of forecast data to data sequences.
        /// </summary>
        /// <param name="city">City data class instance for parse it.</param>
        /// <returns>Enumerable with list of forecasts, every list is one day.</returns>
        public static IEnumerable<List<ForecastModel.Forecast>> SequencesOfForecast(ForecastModel.City city)
        {
            int day = -1;
            var sequences = new List<List<ForecastModel.Forecast>>();
            for (var i = 0; i < city.Forecasts.Count; i++)
            {
                var forecast = city.Forecasts[i];
                var sequenceDay = DateTime.Parse(forecast.LocalTime).Date.Day;

                if (i == 0)
                {
                    day = sequenceDay;
                    sequences.Add(new List<ForecastModel.Forecast> { forecast });
                }
                else
                {
                    if (day == sequenceDay) sequences.Last().Add(forecast);
                    else sequences.Add(new List<ForecastModel.Forecast> { forecast });
                    day = sequenceDay;
                }
            }

            return sequences;
        }
    }
}
