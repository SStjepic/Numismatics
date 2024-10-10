using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.DTO;
using Numismatics.WPF.ViewModels.CountryViewModel;
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
    /// Interaction logic for CountryView.xaml
    /// </summary>
    public partial class CountryView : Window
    {

        public CountryViewModel CountryViewModel { get; set; }
        public CountryDataViewModel CurrentCountry {  get; set; }
        private bool _update;
        public CountryView(CountryDTO? country)
        {
            InitializeComponent();
            CountryViewModel = new CountryViewModel();
            CurrentCountry = new CountryDataViewModel(country);
            DataContext = this;

            _update = country == null ? false : true;
            SetComboBox();
        }

        private void AddCountry(object sender, RoutedEventArgs e)
        {
            
            if (CurrentCountry.IsValid)
            {
                if(_update)
                {
                    var newCountry =  CountryViewModel.UpdateCountry(CurrentCountry.ToCountryDTO());
                    CurrentCountry = new CountryDataViewModel(newCountry);
                }
                else
                {
                    var newCountry = CountryViewModel.CreateCountry(CurrentCountry.ToCountryDTO());
                    CurrentCountry = new CountryDataViewModel(newCountry);
                }
                Close();
            }
            else
            {

            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetComboBox()
        {
            foreach(Era era in Enum.GetValues(typeof(Era)))
            {
                StartYearEraCB.Items.Add(era);
                EndYearEraCB.Items.Add(era);
            }
            if (_update)
            {
                StartYearEraCB.SelectedItem = CurrentCountry.StartYearEra;
                EndYearEraCB.SelectedItem = CurrentCountry.EndYearEra;
            }

        }
    }
}
