using Microsoft.Win32;
using Numismatics.WPF.ViewModels.CountryViewModels;
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
using Numismatics.WPF.ViewModels.NationalCurrencyViewModels;
using Numismatics.WPF.ViewModels.CoinViewModels;
using Numismatics.CORE.Domains.Enums;

namespace Numismatics.WPF.View.CoinView
{
    /// <summary>
    /// Interaction logic for CoinDetailsPage.xaml
    /// </summary>
    public partial class CoinDetailsPage : Window
    {
        public CoinCrudViewModel CoinCrudViewModel { get; set; }
        public NationalCurrencyCrudViewModel NationalCurrencyCrudViewModel { get; set; }

        private bool _isUpdate;
        public CoinDetailsPage(CoinDataViewModel coin)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CoinCrudViewModel = new CoinCrudViewModel(coin);
            NationalCurrencyCrudViewModel = new NationalCurrencyCrudViewModel();
            _isUpdate = coin != null? true: false;
            this.DataContext = this;

            SetComboBox();
        }

        private void SetComboBox()
        {
            foreach (Era era in Enum.GetValues(typeof(Era)))
            {
                EraComboBox.Items.Add(era);
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
