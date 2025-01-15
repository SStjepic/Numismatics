using Microsoft.VisualBasic;
using Numismatic.WPF.ViewModel.CountryViewModel;
using Numismatics.CORE.Domain.Enum;
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

            SetComboBox();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetComboBox()
        {
            foreach (Era era in Enum.GetValues(typeof(Era)))
            {
                StartYearEraComboBox.Items.Add(era);
                EndYearEraComboBox.Items.Add(era);
            }
            if (CountryCrudViewModel.CurrentCountry != null)
            {
                StartYearEraComboBox.SelectedItem = CountryCrudViewModel.CurrentCountry.StartYearEra;
                EndYearEraComboBox.SelectedItem = CountryCrudViewModel.CurrentCountry.EndYearEra;
            }

        }
    }
}
