using Numismatic.WPF.ViewModel.CountryViewModel;
using Numismatic.WPF.ViewModel.CurrencyViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Numismatic.WPF.View.CurrencyView
{
    /// <summary>
    /// Interaction logic for CurrencyPage.xaml
    /// </summary>
    public partial class CurrencyPage : Page
    {
        public CurrencyDisplayViewModel CurrencyDisplayViewModel { get; set; }

        public CurrencyPage()
        {
            InitializeComponent();
            CurrencyDisplayViewModel = new CurrencyDisplayViewModel();
            DataContext = this;
        }

        private void ShowCurrency(object sender, MouseButtonEventArgs e)
        {
            CurrencyDisplayViewModel.UpdateCurrencyCommand.Execute(e);
        }
    }
}
