using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using WeatherSample.Models;
using WeatherSample.Services;
using WeatherSample.Utils;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace WeatherSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ForecastProviderService _service;
        private ForecastModel.City? _data;
        private List<List<ForecastModel.Forecast>> _dataSequences;

#pragma warning disable 8618
        public MainWindowViewModel(ForecastProviderService service)
        {
            _service = service;
            FetchCommand.Execute();
        }
#pragma warning restore 8618

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

        private int _selectedDay = -1;

        public int SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value, SelectedDayChanged);
        }

        private void SelectedDayChanged()
        {
            HourlyData.Clear();
            if (SelectedDay == -1) return;
            foreach (var forecast in _dataSequences[SelectedDay])
            {
                HourlyData.Add(
                    new DisplayHourlyModel
                    {
                        Temp = $"{forecast.Temp}°C",
                        Description = forecast.Description,
                        Time = DateTime.Parse(forecast.LocalTime).ToShortTimeString(),
                        Wind = $"Wind {forecast.WindSpeed}m/h"
                    }
                );
            }
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

        private ObservableCollection<DisplayHourlyModel> _hourlyData = new ObservableCollection<DisplayHourlyModel>();

        public ObservableCollection<DisplayHourlyModel> HourlyData
        {
            get => _hourlyData;
            set => SetProperty(ref _hourlyData, value);
        }

        private DelegateCommand _fetchCommand;
        private bool CanExecuteFetchCommand() => !string.IsNullOrEmpty(SelectedCity);

        public DelegateCommand FetchCommand => _fetchCommand ??= new DelegateCommand(
            ExecuteFetchCommand, CanExecuteFetchCommand
        );

        private async void ExecuteFetchCommand()
        {
            _data = await _service.ForecastOf(SelectedCity);
            if (_data == null)
            {
                MessageBox.Show($"Hey, city with name {SelectedCity} not found.");
            }
            else
            {
                CurrentCity = _data.CityName;
                CurrentTemp = _data.Forecasts.First().Temp.ToString(CultureInfo.InvariantCulture);
                CurrentWeather = _data.Forecasts.First().Description;
                UpdatedAt = _data.Forecasts.First().LocalTime;

                MetaData.Clear();
                MetaData.Add(new DisplayMetaModel {Value = $"Feels like {_data.Forecasts.First().FeelsLike}°C"});
                MetaData.Add(new DisplayMetaModel {Value = $"Wind {_data.Forecasts.First().WindSpeed}m/h"});
                MetaData.Add(new DisplayMetaModel {Value = $"Humidity {_data.Forecasts.First().Humidity}%"});
                MetaData.Add(new DisplayMetaModel {Value = $"Pressure {_data.Forecasts.First().Pressure}mb"});

                DailyData.Clear();
                _dataSequences = ParseUtils.SequencesOfForecast(_data);

                foreach (var sequence in _dataSequences)
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

                SelectedDay = 0;
            }
        }
    }
}