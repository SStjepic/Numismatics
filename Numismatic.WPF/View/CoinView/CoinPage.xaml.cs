using Numismatic.WPF.ViewModel.CoinViewModel;
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

namespace Numismatic.WPF.View.CoinView
{
    /// <summary>
    /// Interaction logic for CoinPage.xaml
    /// </summary>
    public partial class CoinPage : Page
    {
        public CoinDisplayViewModel CoinDisplayViewModel { get; set; }
        public CoinPage()
        {
            InitializeComponent();

            CoinDisplayViewModel = new CoinDisplayViewModel();
            DataContext = this;
        }

        private void ShowCoin(object sender, MouseButtonEventArgs e)
        {
            CoinDisplayViewModel.UpdateCoinCommand.Execute(CoinDisplayViewModel);
        }
    }
}
