using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Cryptor.Services;
using Cryptor.ViewModel;

namespace Cryptor
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            builder.RegisterType<CryptorVM>();
            builder.RegisterType<CoinMarketCapDataProvider>().As<IDataProvider>();
            var container = builder.Build();
            var component = container.Resolve<CryptorVM>();

            Monitor monitor = new Monitor();
            monitor.InitializeComponent();
            monitor.DataContext = component;
            monitor.Show();
        }
    }
}
