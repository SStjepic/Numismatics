using Microsoft.Win32;
using Numismatics.WPF.ViewModel.CountryViewModel;
using Numismatics.CORE.Domain.Enum;
using Numismatics.WPF.ViewModel.CoinViewModel;
using Numismatics.WPF.ViewModel.NationalCurrencyViewModel;
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

namespace Numismatics.WPF.View.CoinView
{
    /// <summary>
    /// Interaction logic for CoinDetailsPage.xaml
    /// </summary>
    public partial class CoinDetailsPage : Window
    {
        public CoinCrudViewModel CoinCrudViewModel { get; set; }
        public NationalCurrencyCrudViewModel NationalCurrencyCrudViewModel { get; set; }
        public CoinDetailsPage(CoinDataViewModel coin)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CoinCrudViewModel = new CoinCrudViewModel(coin);
            NationalCurrencyCrudViewModel = new NationalCurrencyCrudViewModel();
            this.DataContext = this;

            SetComboBox();
        }

        private void SetComboBox()
        {
            foreach (Era era in Enum.GetValues(typeof(Era)))
            {
                EraComboBox.Items.Add(era);
            }
            if (CoinCrudViewModel.CurrentCoin != null)
            {
                EraComboBox.SelectedItem = CoinCrudViewModel.CurrentCoin.Era;
            }
            foreach (MoneyQuality banknoteQuality in Enum.GetValues(typeof(MoneyQuality)))
            {
                CoinQualityComboBox.Items.Add(banknoteQuality);
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
