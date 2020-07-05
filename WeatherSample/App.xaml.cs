using System.Windows;
using Prism.Ioc;
using WeatherSample.Services;
using WeatherSample.Views;

namespace WeatherSample
{
    public partial class App
    {
        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ForecastProviderService>();
        }
    }
}