using Numismatics.CORE.DTOs;
using Numismatics.CORE.Services;
using Numismatics.WPF.Utils;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModels.NationalCurrencyViewModels
{
    public class NationalCurrencyCrudViewModel: INotifyPropertyChanged
    {
        private NationalCurrencyService _nationalCurrencyService;
        private CountryService _countryService;

        private ObservableCollection<CountryDataViewModel> _allCountries;
        public ObservableCollection<CountryDataViewModel> AllCountries
        {
            get => _allCountries;
            set
            {
                _allCountries = value;
                OnPropertyChanged(nameof(AllCountries));
            }
        }

        private CountryDataViewModel _selectedCountry;
        public CountryDataViewModel SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private bool _isUpdate;
        private NationalCurrencyDataViewModel _nationalCurrency;
        public NationalCurrencyDataViewModel NationalCurrency
        {
            get => _nationalCurrency;
            set
            {
                _nationalCurrency = value;
                OnPropertyChanged(nameof(NationalCurrency));
            }
        }

        private ObservableCollection<CountryDataViewModel> _selectedCountries;
        public ObservableCollection<CountryDataViewModel> SelectedCountries
        {
            get => _selectedCountries;
            set
            {
                _selectedCountries = value;
                OnPropertyChanged(nameof(SelectedCountries));
            }
        }
        public ICommand SelectCountryCommand {  get; set; }
        public ICommand RemoveCountryCommand { get; set; }
        public ICommand SaveNationalCurrencyCommand { get; set; }

        public NationalCurrencyCrudViewModel(CurrencyDataViewModel currencyData)
        {
            setup();
            getAllCountries();
            _isUpdate = currencyData != null ? true : false;
            if (currencyData != null)
            {
                var nationalCurrencyDTO = _nationalCurrencyService.Get(currencyData.Id);
                if (nationalCurrencyDTO != null) 
                {
                    NationalCurrency.Id = currencyData.Id;
                    NationalCurrency.Currency = new CurrencyDataViewModel(nationalCurrencyDTO.Currency);
                    NationalCurrency.Countries = convertCountries(nationalCurrencyDTO.CountryDTOs);
                }
            }
        }

        private void setup()
        {
            _nationalCurrencyService = new NationalCurrencyService();
            _countryService = new CountryService();
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            SelectedCountries = new ObservableCollection<CountryDataViewModel>();
            NationalCurrency = new NationalCurrencyDataViewModel();
            SelectCountryCommand = new RelayCommand(c => SelectCountry());
            RemoveCountryCommand = new RelayCommand(c => RemoveCountry());
            SaveNationalCurrencyCommand = new RelayCommand(s => SaveNationalCurrency());

        }
        private List<CountryDataViewModel> convertCountries(List<CountryDTO> countryDTOs)
        {
            List<CountryDataViewModel> result = new List<CountryDataViewModel>();
            foreach (var country in countryDTOs) 
            {
                var countryDVM = new CountryDataViewModel(country);
                result.Add(countryDVM);
                SelectedCountries.Add(countryDVM);
            }
            return result;
        }
        public void SelectCountry()
        {
            if (SelectedCountry == null)
            {
                MessageBox.Show($"You must select a country.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!NationalCurrency.Countries.Contains(SelectedCountry))
            {
                NationalCurrency.Countries.Add(SelectedCountry);
                SelectedCountries.Add(SelectedCountry);
            }
            else
            {
                MessageBox.Show($"The country  \"{SelectedCountry.Name}\" is already in the list.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void RemoveCountry()
        {
            if (SelectedCountry == null)
            {
                MessageBox.Show($"You must select a country.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (NationalCurrency.Countries.Contains(SelectedCountry))
            {
                NationalCurrency.Countries.Remove(SelectedCountry);
                SelectedCountries.Remove(SelectedCountry);
            }
            else
            {
                MessageBox.Show($"The country  \"{SelectedCountry.Name}\" is not in the list.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public NationalCurrencyCrudViewModel() 
        {
            setup();
            getAllCountries();
        }

        private void getAllCountries()
        {
            foreach (var country in _countryService.GetAll())
            {
                AllCountries.Add(new CountryDataViewModel(country));
            }
        }

        public void SaveNationalCurrency()
        {
            NationalCurrencyDTO nationalCurrencyDTO = new NationalCurrencyDTO();
            nationalCurrencyDTO.Id = _isUpdate? NationalCurrency.Id: -1;
            nationalCurrencyDTO.Currency = NationalCurrency.Currency.ToCurrencyDTO();
            nationalCurrencyDTO.CountryDTOs = NationalCurrency.Countries
                .Select(c => c.ToCountryDTO())
                .ToList();
            if (_isUpdate) 
            {
                _nationalCurrencyService.Update(nationalCurrencyDTO);
            }
            else
            {
                _nationalCurrencyService.Create(nationalCurrencyDTO);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
