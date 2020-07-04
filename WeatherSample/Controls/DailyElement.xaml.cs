using System.Windows;

namespace WeatherSample.Controls
{
    /// <summary>
    /// Daily element control, uses in daily weather
    /// container.
    /// </summary>
    public partial class DailyElement
    {
        public DailyElement()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Display temperature value in this control.
        /// </summary>
        public string? DisplayTemp
        {
            get => (string) GetValue(DisplayTempProperty);
            set => SetValue(DisplayTempProperty, value);
        }

        public static readonly DependencyProperty DisplayTempProperty =
            DependencyProperty.Register(
                nameof(DisplayTemp), typeof(string),
                typeof(DailyElement),
                new PropertyMetadata(null)
            );


        public string DayName
        {
            get { return (string)GetValue(DayNameProperty); }
            set { SetValue(DayNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DayName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayNameProperty =
            DependencyProperty.Register("DayName", typeof(string), typeof(DailyElement), new PropertyMetadata(0));



        public string MinTemp
        {
            get { return (string)GetValue(MinTempProperty); }
            set { SetValue(MinTempProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinTemp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinTempProperty =
            DependencyProperty.Register("MinTemp", typeof(string), typeof(DailyElement), new PropertyMetadata(0));



        public string MaxTemp
        {
            get { return (string)GetValue(MaxTempProperty); }
            set { SetValue(MaxTempProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxTemp.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxTempProperty =
            DependencyProperty.Register("MaxTemp", typeof(string), typeof(DailyElement), new PropertyMetadata(0));



        public string WeatherType
        {
            get { return (string)GetValue(WeatherTypeProperty); }
            set { SetValue(WeatherTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WeatherType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeatherTypeProperty =
            DependencyProperty.Register("WeatherType", typeof(string), typeof(DailyElement), new PropertyMetadata(0));


    }   
}