using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using WeatherSample.Models;
using WeatherSample.Services;
using WeatherSample.Utils;

namespace WeatherSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ForecastProviderService _service;

        public MainWindowViewModel(ForecastProviderService service)
        {
            _service = service;
            FetchCommand.Execute();
        }

        private string _selectedCity = "Bryansk";
        private void SelectedCityChanged() => FetchCommand.RaiseCanExecuteChanged();

        public string SelectedCity
        {
            get => _selectedCity;
            set => SetProperty(ref _selectedCity, value, SelectedCityChanged);
        }

        private string? _currentCity;

        public string? CurrentCity
        {
            get => _currentCity;
            set => SetProperty(ref _currentCity, $"Now in {value}");
        }

        private string? _currentTemp;

        public string? CurrentTemp
        {
            get => _currentTemp;
            set => SetProperty(ref _currentTemp, $"{value}°C");
        }

        private string? _currentWeather;

        public string? CurrentWeather
        {
            get => _currentWeather;
            set => SetProperty(ref _currentWeather, value);
        }

        private string? _updatedAt;

        public string? UpdatedAt
        {
            get => _updatedAt;
            set => SetProperty(ref _updatedAt, $"Data updated at {value}");
        }

        private ObservableCollection<DisplayMetaModel> _metaData = new ObservableCollection<DisplayMetaModel>();

        public ObservableCollection<DisplayMetaModel> MetaData
        {
            get => _metaData;
            set => SetProperty(ref _metaData, value);
        }

        private ObservableCollection<DisplayDailyModel> _dailyData = new ObservableCollection<DisplayDailyModel>();

        public ObservableCollection<DisplayDailyModel> DailyData
        {
            get => _dailyData;
            set => SetProperty(ref _dailyData, value);
        }

        private DelegateCommand _fetchCommand;

        public DelegateCommand FetchCommand =>
            _fetchCommand ??= new DelegateCommand(ExecuteFetchCommand, CanExecuteFetchCommand);

        private bool CanExecuteFetchCommand() => !string.IsNullOrEmpty(SelectedCity);

        private async void ExecuteFetchCommand()
        {
            var result = await _service.ForecastOf(SelectedCity);
            if (result == null)
            {
                MessageBox.Show($"Hey, city with name {SelectedCity} not found.");
            }
            else
            {
                CurrentCity = result.CityName;
                CurrentTemp = result.Forecasts.First().Temp.ToString(CultureInfo.InvariantCulture);
                CurrentWeather = result.Forecasts.First().Description;
                UpdatedAt = result.Forecasts.First().LocalTime;

                MetaData.Clear();
                MetaData.Add(new DisplayMetaModel {Value = $"Feels like {result.Forecasts.First().FeelsLike}°C"});
                MetaData.Add(new DisplayMetaModel {Value = $"Wind {result.Forecasts.First().WindSpeed * 3.6}km/h"});
                MetaData.Add(new DisplayMetaModel {Value = $"Humidity {result.Forecasts.First().Humidity}%"});
                MetaData.Add(new DisplayMetaModel {Value = $"Pressure {result.Forecasts.First().Pressure}mb"});

                DailyData.Clear();
                foreach (var sequence in ParseUtils.SequencesOfForecast(result))
                {
                    var date = DateTime.Parse(sequence.First().LocalTime);
                    DailyData.Add(
                        new DisplayDailyModel
                        {
                            DayName = $"{date.DayOfWeek} {date.Day}",
                            High = $"{sequence.Max(forecast => forecast.TempMax)}°",
                            Low = $"{sequence.Min(forecast => forecast.TempMin)}°",
                            Description = sequence[sequence.Count / 2].Description
                        }
                    );
                }
            }
        }
    }
}