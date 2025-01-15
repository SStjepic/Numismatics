using Numismatic.WPF.ViewModel.CountryViewModel;
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

namespace Numismatic.WPF.View.CountryView
{
    /// <summary>
    /// Interaction logic for CountryDetailsPage.xaml
    /// </summary>
    public partial class CountryDetailsPage : Window
    {
        public CountryCrudViewModel CountryCrudViewModel { get; set; }
        public CountryDetailsPage(CountryDataViewModel country)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CountryCrudViewModel = new CountryCrudViewModel(country);
            DataContext = this;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
