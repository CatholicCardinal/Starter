using Microsoft.Extensions.DependencyInjection;
using Starter.Deserialization.Factory;
using Starter.Models.Repositories;
using Starter.Serialization.Factory;
using System.Windows;
using IOC = Microsoft.Toolkit.Mvvm.DependencyInjection.Ioc;


namespace Starter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IOC.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<ISerializatorFactory, SerializatorFactory>()
                .AddSingleton<IUnitOfWork, UnitOfWork>()
                .AddSingleton<IDeserializatorFactory, DeserializatorFactory>()
                .BuildServiceProvider());
        }
    }
}
