using Numismatics.CORE.Domains.Models;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using Numismatics.WPF.ViewModels.NationalCurrencyViewModels;
using System.Windows;

namespace Numismatics.WPF.View.CurrencyView
{
    /// <summary>
    /// Interaction logic for CurrencyDetailsPage.xaml
    /// </summary>
    public partial class CurrencyDetailsPage : Window
    {
        public CurrencyCrudViewModel CurrencyCrudViewModel { get; set; }
        public NationalCurrencyCrudViewModel NationalCurrencyCrudViewModel { get; set; }
        public CurrencyDetailsPage(CurrencyDataViewModel currency)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CurrencyCrudViewModel = new CurrencyCrudViewModel(currency);
            NationalCurrencyCrudViewModel = new NationalCurrencyCrudViewModel(currency);
            DataContext = this;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            CurrencyCrudViewModel.AddCurrencyCommand.Execute(this);
            NationalCurrencyCrudViewModel.CurrencyDataViewModel = CurrencyCrudViewModel.CurrentCurrency;
            NationalCurrencyCrudViewModel.SaveNationalCurrencyCommand.Execute(this);
        }
    }
}
