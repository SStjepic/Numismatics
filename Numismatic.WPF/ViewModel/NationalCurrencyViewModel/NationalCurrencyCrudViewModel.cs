using Numismatic.WPF.ViewModel.CountryViewModel;
using Numismatic.WPF.ViewModel.CurrencyViewModel;
using Numismatic.WPF.ViewModels;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Numismatic.WPF.ViewModel.NationalCurrencyViewModel
{
    public class NationalCurrencyCrudViewModel: INotifyPropertyChanged
    {
        private NationalCurrencyService _nationalCurrencyService;
        private CountryService _countryService;

        private ObservableCollection<CountryDataViewModel> _allCountries;
        public ObservableCollection<CountryDataViewModel> AllCountries
        {
            get => this._allCountries;
            set
            {
                this._allCountries = value;
                OnPropertyChanged(nameof(AllCountries));
            }
        }
        private ObservableCollection<CountryDataViewModel> _selectedCountries;
        public ObservableCollection<CountryDataViewModel> SelectedCountries
        {
            get => this._selectedCountries;
            set
            {
                this._selectedCountries = value;
                OnPropertyChanged(nameof(SelectedCountries));
            }
        }

        private CountryDataViewModel _selectedCountry;
        public CountryDataViewModel SelectedCountry
        {
            get => this._selectedCountry;
            set
            {
                this._selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private ObservableCollection<CurrencyDataViewModel> _currentCurrencies;
        public ObservableCollection<CurrencyDataViewModel> CurrentCurrencies
        {
            get => this._currentCurrencies;
            set
            {
                this._currentCurrencies = value;
                OnPropertyChanged(nameof(CurrentCurrencies));
            }
        }
        private CurrencyDataViewModel _currentCurrency;
        public CurrencyDataViewModel CurrentCurrency
        {
            get => this._currentCurrency;
            set
            {
                this._currentCurrency = value;
                OnPropertyChanged(nameof(CurrentCurrency));
            }
        }

        public ICommand SelectCountryCommand {  get; set; }
        public ICommand SaveNationalCurrencyCommand { get; set; }

        private void setup()
        {
            _nationalCurrencyService = new NationalCurrencyService();
            _countryService = new CountryService();
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            SelectedCountries = new ObservableCollection<CountryDataViewModel>();
            SelectCountryCommand = new RelayCommand(c => SelectCountry());
            SaveNationalCurrencyCommand = new RelayCommand(s => SaveNationalCurrency());

        }

        public void SelectCountry()
        {
            if (!SelectedCountries.Contains(SelectedCountry))
            {
                SelectedCountries.Add(SelectedCountry);
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

        private void getCountriesByCurrency(int currencyId)
        {
            foreach (var country in _nationalCurrencyService.GetCountries(currencyId))
            {
                SelectedCountries.Add(new CountryDataViewModel(country));
            }
        }

        public NationalCurrencyCrudViewModel(CurrencyDataViewModel currencyData)
        {
            setup();
            getAllCountries();
            if (currencyData != null)
            {
                CurrentCurrency = currencyData;
                getCountriesByCurrency(currencyData.Id);
            }
        }

        public void SaveNationalCurrency()
        {
            NationalCurrencyDTO nationalCurrencyDTO = new NationalCurrencyDTO();
            nationalCurrencyDTO.Currency = CurrentCurrency.ToCurrencyDTO();
            nationalCurrencyDTO.CountryDTOs = SelectedCountries
                .Select(c => c.ToCountryDTO())
                .ToList();
            _nationalCurrencyService.Create(nationalCurrencyDTO);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
