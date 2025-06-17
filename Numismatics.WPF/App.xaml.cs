using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Numismatics.CORE.Repositories;
using Numismatics.CORE.Services;
using Numismatics.CORE.Services.Interface;
using Numismatics.INFRASTRUCTURE.Repositories.FileStorage;
using Numismatics.INFRASTRUCTURE.Repositories.JSON;
using Numismatics.INFRASTRUCTURE.Repositories.SQLite;
using Numismatics.WPF.ViewModels.BanknoteViewModels;
using Numismatics.WPF.ViewModels.CoinViewModels;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.WPF.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App() 
        {
            AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<SQLRepositoryContext>(options =>
                    options.UseSqlite("Data Source=numismatics.db"));

                //Repositories
                services.AddTransient<ICoinRepository, SQLiteCoinRepository>();
                services.AddTransient<IBanknoteRepository, SQLiteBanknoteRepository>();
                services.AddTransient<ICurrencyRepository, SQLiteCurrencyRepository>();
                services.AddTransient<ICountryRepository, SQLiteCountryRepository>();
                services.AddTransient<INationalCurrencyRepository, SQLiteNationalCurrencyRepository>();
                services.AddTransient<IImageRepository, ImageRepository>();
                services.AddTransient<IOwnedBanknotesRepository, SQLiteOwnedBanknoteRepository>();
                services.AddTransient<IOwnedCoinRepository, SQLiteOwnedCoinRepository>();


                // Services
                services.AddTransient<ICoinService, CoinService>();
                services.AddTransient<IBanknoteService, BanknoteService>();
                services.AddTransient<ICurrencyService, CurrencyService>();
                services.AddTransient<ICountryService, CountryService>();
                services.AddTransient<INationalCurrencyService, NationalCurrencyService>();

                //VW
                services.AddSingleton<MainNavigationViewModel>();

                services.AddSingleton<MainWindow>();
            })
            .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

    }
}
