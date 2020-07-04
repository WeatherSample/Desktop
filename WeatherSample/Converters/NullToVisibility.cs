using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WeatherSample.Converters
{
    /// <summary>
    /// Null to visibility converter class implementation.
    /// </summary>
    public class NullToVisibility : IValueConverter
    {
        /// <summary>
        /// Do convert null to specified visibility state.
        /// </summary>
        /// <param name="value">Nullable value to convert.</param>
        /// <param name="targetType">Check inherited documentations.</param>
        /// <param name="parameter">Check inherited documentations.</param>
        /// <param name="culture">Check inherited documentations.</param>
        /// <returns>Collapsed visibility state if value is null otherwise visible state.</returns>
        public object Convert(
            object? value, Type targetType, object parameter, CultureInfo culture
        ) => value == null ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture
        ) => throw new NotImplementedException();
    }
}