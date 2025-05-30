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
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.Domain.Enum;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using Numismatics.WPF.Util;
using Numismatics.WPF.ViewModel.CurrencyViewModel;
using Numismatics.WPF.ViewModel.CountryViewModel;

namespace Numismatics.WPF.ViewModel.CoinViewModel
{
    public class CoinCrudViewModel : INotifyPropertyChanged
    {
        private CoinService _coinService;
        private CountryService _countryService;
        private NationalCurrencyService _nationalCurrencyService;

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

        private List<string> _currencyValue;
        public List<string> CurrencyValue
        {
            get => _currencyValue;
            set
            {
                _currencyValue = value;
                OnPropertyChanged(nameof(CurrencyValue));
            }
        }

        private string _selectedCurrencyValue;
        public string SelectedCurrencyValue
        {
            get => _selectedCurrencyValue;
            set
            {
                _selectedCurrencyValue = value;
                if (value.Equals(CurrentCoin.Currency.HunderthPartName)) 
                {
                    CurrentCoin.HundertPart = value;
                }
                else
                {
                    CurrentCoin.HundertPart = "";
                }
                    OnPropertyChanged(nameof(SelectedCurrencyValue));
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
        public string NumberOfCoins => $"Number of coins: {CurrentCoin.Coins.Sum(pair => pair.Value)}";

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
            _coinService = new CoinService();
            _countryService = new CountryService();
            _nationalCurrencyService = new NationalCurrencyService();

            CurrentCoin = coin != null ? coin : new CoinDataViewModel(null);
            _isUpdate = coin != null ? true : false;
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            Currencies = new ObservableCollection<CurrencyDataViewModel>();
            CurrencyValue = new List<string>();

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
                if (CurrentCoin.HundertPart == "")
                {
                    SelectedCurrencyValue = CurrencyValue.FirstOrDefault(cv => cv.Equals(CurrentCoin.Currency.Name));
                }
                else
                {
                    SelectedCurrencyValue = CurrencyValue.FirstOrDefault(cv => cv.Equals(CurrentCoin.Currency.HunderthPartName));
                }
                OnPropertyChanged(nameof(SelectedCurrencyValue));
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
            foreach( var currency in _nationalCurrencyService.GetCurrencies(country.Id))
            {
                Currencies.Add(new CurrencyDataViewModel(currency));
            }
        }

        private void getCurrencyValue(CurrencyDataViewModel currency)
        {
            CurrencyValue.Add(currency.Name);
            CurrencyValue.Add(currency.HunderthPartName);
        }

        private bool createCoin()
        {
            if (CurrentCoin.IsValid)
            {
                if (_isUpdate)
                {
                    _coinService.Update(CurrentCoin.ToCoinDTO());
                    MessageBox.Show("You successfully update coin.", "Excelent");
                }
                else
                {
                    _coinService.Create(CurrentCoin.ToCoinDTO());
                    MessageBox.Show("You successfully add new coin.", "Excelent");
                }
                Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)!.DialogResult = true;
                return true;
            }
            return false;
        }

        private void addCoinQuality()
        {
            if (CurrentCoin.CurrentCoinQuality != null)
            {
                MoneyQuality coinQuality = (MoneyQuality)Enum.Parse(typeof(MoneyQuality), CurrentCoin.CurrentCoinQuality);
                QualityKeyValuePair<MoneyQuality, int> pair = CurrentCoin.Coins.FirstOrDefault(p => p.Key.Equals(coinQuality));
                if (pair == null)
                {
                    CurrentCoin.Coins.Add(new QualityKeyValuePair<MoneyQuality, int>(coinQuality, 1));
                }
                else
                {
                    var number = pair.Value;
                    CurrentCoin.Coins.Remove(pair);
                    CurrentCoin.Coins.Add(new QualityKeyValuePair<MoneyQuality, int>(coinQuality, number + 1));
                }
                OnPropertyChanged(nameof(NumberOfCoins));
            }
            else
            {
                MessageBox.Show("Choose coin quality", "Error");
            }

        }
        private void deleteCoinQuality()
        {
            if (CurrentCoin.CurrentCoinQualityPair != null)
            {
                if (CurrentCoin.CurrentCoinQualityPair.Value > 1)
                {
                    int indexAt = CurrentCoin.Coins.IndexOf(CurrentCoin.CurrentCoinQualityPair);
                    CurrentCoin.Coins[indexAt].Value--;
                }
                else
                {
                    CurrentCoin.Coins.Remove(CurrentCoin.CurrentCoinQualityPair);
                }
                OnPropertyChanged(nameof(NumberOfCoins));
            }
            else
            {
                MessageBox.Show("Please select coin <quality,number> pair", "Error");
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
