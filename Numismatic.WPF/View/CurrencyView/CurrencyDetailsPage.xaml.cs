using Numismatic.WPF.ViewModel.CountryViewModel;
using Numismatic.WPF.ViewModel.CurrencyViewModel;
using Numismatic.WPF.ViewModel.NationalCurrencyViewModel;
using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Numismatic.WPF.View.CurrencyView
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
            NationalCurrencyCrudViewModel.SaveNationalCurrencyCommand.Execute(this);
        }
    }
}
