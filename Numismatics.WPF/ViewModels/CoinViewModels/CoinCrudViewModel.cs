using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using Numismatics.WPF.Utils;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Numismatics.WPF.ViewModels.CoinViewModels
{
    public class CoinCrudViewModel : INotifyPropertyChanged
    {
        private ICoinService _coinService;
        private ICountryService _countryService;
        private INationalCurrencyService _nationalCurrencyService;

        private CoinDataViewModel _currentCoin;
        public CoinDataViewModel CurrentCoin
        {
            get => _currentCoin;
            set
            {
                _currentCoin = value;
                OnPropertyChanged(nameof(CurrentCoin));
            }
        }

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
                CurrentCoin.Country = value;
                getCurrencies(value);
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private ObservableCollection<string> _currencyUnitNames;
        public ObservableCollection<string> CurrencyUnitNames
        {
            get => _currencyUnitNames;
            set
            {
                _currencyUnitNames = value;
                OnPropertyChanged(nameof(CurrencyUnitNames));
            }
        }

        private string _selectedCurrencyUnitName;
        public string SelectedCurrencyUnitName
        {
            get => _selectedCurrencyUnitName;
            set
            {
                _selectedCurrencyUnitName = value;
                if (CurrentCoin.Currency != null && value.Equals(CurrentCoin.Currency.SubunitName)) 
                {
                    CurrentCoin.UnitName = value;
                }
                else
                {
                    CurrentCoin.UnitName = CurrentCoin.Currency.MainUnitName;
                }
                OnPropertyChanged(nameof(SelectedCurrencyUnitName));
            }
        }

        private ObservableCollection<CurrencyDataViewModel> _currencies;
        public ObservableCollection<CurrencyDataViewModel> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        private CurrencyDataViewModel _selectedCurrency;
        public CurrencyDataViewModel SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                CurrentCoin.Currency = value;
                getCurrencyValue(value);
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }
        public string NumberOfCoins => $"Number of coins: {CurrentCoin.Coins.Sum(pair => pair.NumberOfCoins)}";

        private bool _isUpdate;

        public ICommand AddCoinCommand { get; set; }
        public ICommand AddCoinQualityCommand { get; set; }
        public ICommand DeleteCoinQualityCommand { get; set; }
        public ICommand AddObversePictureCommand {  get; set; }
        public ICommand DeleteObversePictureCommand { get; set; }
        public ICommand AddReversePictureCommand {  get; set; }
        public ICommand DeleteReversePictureCommand { get; set; }
        public CoinCrudViewModel(CoinDataViewModel coin)
        {
            _coinService = App.AppHost.Services.GetRequiredService<ICoinService>();
            _countryService = App.AppHost.Services.GetRequiredService<ICountryService>();
            _nationalCurrencyService = App.AppHost.Services.GetRequiredService<INationalCurrencyService>();

            CurrentCoin = coin != null ? coin : new CoinDataViewModel(null);
            _isUpdate = coin != null ? true : false;
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            Currencies = new ObservableCollection<CurrencyDataViewModel>();
            CurrencyUnitNames = new ObservableCollection<string>();

            AddCoinCommand = new RelayCommand(c => createCoin());
            AddCoinQualityCommand = new RelayCommand(c =>  addCoinQuality());
            DeleteCoinQualityCommand = new RelayCommand(c => deleteCoinQuality());
            AddObversePictureCommand = new RelayCommand(c => addObversePicture());
            DeleteObversePictureCommand = new RelayCommand(c => deleteObversePicture());
            AddReversePictureCommand = new RelayCommand(c => addReversePicture());
            DeleteReversePictureCommand = new RelayCommand(c=>  deleteReversePicture());

            getAllCountries();
            setupUI();
        }

        private void setupUI()
        {
            if (_isUpdate)
            {
                SelectedCountry = AllCountries.FirstOrDefault(c => c.Id == CurrentCoin.Country.Id);
                OnPropertyChanged(nameof(SelectedCountry));
                SelectedCurrency = Currencies.FirstOrDefault(c => c.Id == CurrentCoin.Currency.Id);
                OnPropertyChanged(nameof(SelectedCurrency));
                if(SelectedCurrency != null)
                {
                    if (CurrentCoin.UnitName == CurrentCoin.Currency.MainUnitName)
                    {
                        SelectedCurrencyUnitName = CurrencyUnitNames.FirstOrDefault(cv => cv.Equals(CurrentCoin.Currency.MainUnitName));
                    }
                    else
                    {
                        SelectedCurrencyUnitName = CurrencyUnitNames.FirstOrDefault(cv => cv.Equals(CurrentCoin.Currency.SubunitName));
                    }
                    OnPropertyChanged(nameof(SelectedCurrencyUnitName));
                }
            }
        }
        private void getAllCountries()
        {
            foreach (var country in _countryService.GetAll())
            {
                AllCountries.Add(new CountryDataViewModel(country));
            }
        }

        private void getCurrencies(CountryDataViewModel country)
        {
            Currencies.Clear();
            if(country != null)
            {
                foreach (var currency in _nationalCurrencyService.GetCurrencies(country.Id))
                {
                    Currencies.Add(new CurrencyDataViewModel(currency));
                }
            }
        }

        private void getCurrencyValue(CurrencyDataViewModel currency)
        {
            CurrencyUnitNames.Clear();
            if (currency != null) 
            {
                CurrencyUnitNames.Add(currency.MainUnitName);
                CurrencyUnitNames.Add(currency.SubunitName);
                OnPropertyChanged(nameof(CurrencyUnitNames));
            }
        }

        private bool createCoin()
        {
            if (CurrentCoin.IsValid)
            {
                if (_isUpdate)
                {
                    _coinService.Update(CurrentCoin.ToCoinDTO());
                    MessageBox.Show("You successfully update coin.", "Excelent", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _coinService.Create(CurrentCoin.ToCoinDTO());
                    MessageBox.Show("You successfully add new coin.", "Excelent", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)!.DialogResult = true;
                return true;
            }
            else
            {
                MessageBox.Show("You must have at least one coin.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;
        }

        private void addCoinQuality()
        {
            if (CurrentCoin.CurrentCoinQuality != null)
            {
                MoneyQuality coinQuality = (MoneyQuality)Enum.Parse(typeof(MoneyQuality), CurrentCoin.CurrentCoinQuality);
                OwnedCoinDataViewModel ownedCoin = CurrentCoin.Coins.FirstOrDefault(p => p.Quality.Equals(coinQuality));
                if (ownedCoin == null)
                {
                    CurrentCoin.Coins.Add(new OwnedCoinDataViewModel(0, 1, coinQuality,CurrentCoin.Id));
                }
                else
                {
                    CurrentCoin.Coins.First(c => c.Quality.Equals(coinQuality)).NumberOfCoins++;
                }
                OnPropertyChanged(nameof(NumberOfCoins));
            }
            else
            {
                MessageBox.Show("Choose coin quality.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void deleteCoinQuality()
        {
            if (CurrentCoin.CurrentOwnedCoin != null)
            {
                if (CurrentCoin.CurrentOwnedCoin.NumberOfCoins > 1)
                {
                    CurrentCoin.Coins.First(c => c.Quality.Equals(CurrentCoin.CurrentOwnedCoin.Quality)).NumberOfCoins--;
                }
                else
                {
                    CurrentCoin.Coins.Remove(CurrentCoin.CurrentOwnedCoin);
                }
                OnPropertyChanged(nameof(NumberOfCoins));
            }
            else
            {
                MessageBox.Show("Please select a owned coin.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void addObversePicture()
        {
            CurrentCoin.ObversePicture = getFilePath();
        }

        private void deleteObversePicture()
        {
            CurrentCoin.ObversePicture = "";
        }

        private void addReversePicture()
        {
            CurrentCoin.ReversePicture = getFilePath();
        }

        private void deleteReversePicture()
        {
            CurrentCoin.ReversePicture = "";
        }

        private string getFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            fileDialog.FilterIndex = 1;
            if (fileDialog.ShowDialog() == true)
            {
                return fileDialog.FileName;
            }
            return "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
