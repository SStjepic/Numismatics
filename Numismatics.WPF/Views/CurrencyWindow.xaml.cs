using Numismatics.CORE.DTO;
using Numismatics.WPF.ViewModels.CurrencyViewModel;
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

namespace Numismatics.WPF.Views
{
    /// <summary>
    /// Interaction logic for CurrencyWindow.xaml
    /// </summary>
    public partial class CurrencyWindow : Window
    {
        public CurrencyViewModel CurrencyViewModel { get; set; }
        public CurrencyDataViewModel CurrentCurrency { get; set; }
        private bool _update;
        public CurrencyWindow(CurrencyDTO? currencyDTO)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            CurrencyViewModel = new CurrencyViewModel();
            CurrentCurrency = new CurrencyDataViewModel(currencyDTO);
            DataContext = this;
            _update = currencyDTO == null? false : true;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddCurrency(object sender, RoutedEventArgs e)
        {
            if (CurrentCurrency.IsValid)
            {
                if(_update)
                {
                    var newCurrency = CurrencyViewModel.UpdateCurrency(CurrentCurrency.ToCurrencyDTO());
                    CurrentCurrency = new CurrencyDataViewModel(newCurrency);
                }
                else
                {
                    var newCurrency =  CurrencyViewModel.CreateCurrency(CurrentCurrency.ToCurrencyDTO());
                    CurrentCurrency = new CurrencyDataViewModel(newCurrency);
                }
                Close();
                
            }

        }
    }
}
