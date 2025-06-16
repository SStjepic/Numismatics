using Microsoft.Extensions.DependencyInjection;
using Numismatics.CORE.DTOs;
using Numismatics.CORE.Services.Interface;
using Numismatics.WPF.Utils;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModels.NationalCurrencyViewModels
{
    public class NationalCurrencyCrudViewModel: INotifyPropertyChanged
    {
        private INationalCurrencyService _nationalCurrencyService;
        private ICountryService _countryService;

        public CurrencyDataViewModel CurrencyDataViewModel {  get; set; }
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
        private List<NationalCurrencyDataViewModel> _nationalCurrencies;
        public List<NationalCurrencyDataViewModel> NationalCurrencies
        {
            get => _nationalCurrencies;
            set
            {
                _nationalCurrencies = value;
                OnPropertyChanged(nameof(NationalCurrencies));
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
            CurrencyDataViewModel = currencyData;
            _isUpdate = currencyData != null ? true : false;
            if (currencyData != null)
            {
                var nationalCurrencyDTOs = _nationalCurrencyService.GetAll(currencyData.Id);
                if (nationalCurrencyDTOs != null) 
                {
                    foreach(var nationalCurrency in nationalCurrencyDTOs)
                    {
                        NationalCurrencies.Add(new NationalCurrencyDataViewModel(nationalCurrency.Id, new CurrencyDataViewModel(nationalCurrency.Currency), new CountryDataViewModel(nationalCurrency.Country)));
                        SelectedCountries.Add(new CountryDataViewModel(nationalCurrency.Country));
                    }
                }
            }
        }

        private void setup()
        {
            _nationalCurrencyService = App.AppHost.Services.GetRequiredService<INationalCurrencyService>();
            _countryService = App.AppHost.Services.GetRequiredService<ICountryService>();
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            SelectedCountries = new ObservableCollection<CountryDataViewModel>();
            NationalCurrencies = new List<NationalCurrencyDataViewModel>();
            SelectCountryCommand = new RelayCommand(c => SelectCountry());
            RemoveCountryCommand = new RelayCommand(c => RemoveCountry());
            SaveNationalCurrencyCommand = new RelayCommand(s => SaveNationalCurrency());

        }

        public void SelectCountry()
        {
            if (SelectedCountry == null)
            {
                MessageBox.Show($"You must select a country.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!SelectedCountries.Contains(SelectedCountry))
            {
                SelectedCountries.Add(SelectedCountry);
                NationalCurrencies.Add(new NationalCurrencyDataViewModel(-1, CurrencyDataViewModel, SelectedCountry));
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
            else if (SelectedCountries.Contains(SelectedCountry))
            {
                SelectedCountries.Remove(SelectedCountry);
                NationalCurrencies.RemoveAll(x => x.Country.Id == SelectedCountry.Id);
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
            var nationalCurrencies = NationalCurrencies
                .Select(x => x.ToNationalCurrencyDTO())
                .ToList();
            _nationalCurrencyService.UpdateAll(nationalCurrencies, CurrencyDataViewModel.ToCurrencyDTO());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
