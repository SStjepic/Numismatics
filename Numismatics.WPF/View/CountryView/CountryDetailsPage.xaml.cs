using Microsoft.VisualBasic;
using Numismatics.CORE.Domain.Enum;
using Numismatics.WPF.ViewModel.CountryViewModel;
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

namespace Numismatics.WPF.View.CountryView
{
    /// <summary>
    /// Interaction logic for CountryDetailsPage.xaml
    /// </summary>
    public partial class CountryDetailsPage : Window
    {
        public CountryCrudViewModel CountryCrudViewModel { get; set; }

        private bool _isUpdate;
        public CountryDetailsPage(CountryDataViewModel country)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            CountryCrudViewModel = new CountryCrudViewModel(country);
            _isUpdate = country != null? true : false;
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
            if (_isUpdate)
            {
                StartYearEraComboBox.SelectedItem = CountryCrudViewModel.CurrentCountry.StartYearEra;
                EndYearEraComboBox.SelectedItem = CountryCrudViewModel.CurrentCountry.EndYearEra;
            }
            else
            {
                StartYearEraComboBox.SelectedItem = Era.AC;
                EndYearEraComboBox.SelectedItem = Era.AC;
            }

        }
    }
}
