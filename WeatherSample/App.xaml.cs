using Prism.Ioc;
using Prism.Ninject;
using System.Windows;
using WeatherSample.Services;
using WeatherSample.Views;

namespace WeatherSample
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ForecastProviderService>();
        }
    }
}