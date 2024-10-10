using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using Numismatics.WPF.ViewModels.CountryViewModel;
using Numismatics.WPF.ViewModels.CurrencyViewModel;
using Numismatics.WPF.ViewModels.NationalCurrencyViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public NationalCurrencyViewModel NationalCurrencyViewModel { get; set; }
        public CountryViewModel CountryViewModel { get; set; }
        public CurrencyDataViewModel CurrentCurrency { get; set; }

        public ObservableCollection<CountryDTO> Countries { get; set; }
        public ObservableCollection<CountryDTO> SelectedCountries { get; set; }
        private bool _update;
        public CurrencyWindow(CurrencyDTO? currencyDTO)
        {
            InitializeComponent();

            CurrencyViewModel = new CurrencyViewModel();
            CountryViewModel = new CountryViewModel();
            NationalCurrencyViewModel = new NationalCurrencyViewModel();
            CurrentCurrency = new CurrencyDataViewModel(currencyDTO);
            Countries = new ObservableCollection<CountryDTO>();
            SelectedCountries = new ObservableCollection<CountryDTO>();
            DataContext = this;
            _update = currencyDTO == null? false : true;
            SetCountryComboBox();

        }
        private void SetCountryComboBox()
        {
            
            foreach (var country in CountryViewModel.GetAllCountries())
            {
                Countries.Add(country);
            }
            if (_update)
            {
                foreach (var selectedCountry in NationalCurrencyViewModel.GetCountriesByCurrency(CurrentCurrency.ToCurrencyDTO()))
                {
                    SelectedCountries.Add(selectedCountry);
                }
            }
            
            
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetSelectedCountries()
        {
            if(CountryLB.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select at least one country");
                return;
            }
            foreach(var country in CountryLB.SelectedItems)
            {
                SelectedCountries.Add(country as CountryDTO);
            }
        }

        private void AddCurrency(object sender, RoutedEventArgs e)
        {
            GetSelectedCountries();
            if (CurrentCurrency.IsValid && SelectedCountries.Count != 0)
            {
                if(_update)
                {
                    var newCurrency = CurrencyViewModel.UpdateCurrency(CurrentCurrency.ToCurrencyDTO());
                    NationalCurrencyViewModel.CreateNationalCurrency(newCurrency, SelectedCountries.ToList());
                    CurrentCurrency = new CurrencyDataViewModel(newCurrency);
                }
                else
                {
                    
                    var newCurrency =  CurrencyViewModel.CreateCurrency(CurrentCurrency.ToCurrencyDTO());
                    NationalCurrencyViewModel.CreateNationalCurrency(newCurrency, SelectedCountries.ToList());
                    CurrentCurrency = new CurrencyDataViewModel(newCurrency);
                }
                Close();
                
            }

        }

        private void SelectCountry(object sender, RoutedEventArgs e)
        {
            var selectedCountry = CountryComboBox.SelectedItem;
            if(selectedCountry != null)
            {
                if (!SelectedCountries.Contains(selectedCountry))
                {
                    SelectedCountries.Add((CountryDTO)selectedCountry);
                }
            }
            else
            {
                MessageBox.Show("Please, choose country", "Error");
            }
        }
    }
}
