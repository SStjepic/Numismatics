using Numismatics.WPF.View.CountryView;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Numismatics.WPF.View.CoinView;
using Numismatics.WPF.Utils;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.CORE.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Numismatics.WPF.ViewModels.CoinViewModels
{
    public class CoinDisplayViewModel:DisplayViewMode
    {
        private ICoinService _coinService;
        private ICountryService _countryService;
        private ICurrencyService _currencyService;

        private CoinDataViewModel _selectedCoin;
        public CoinDataViewModel SelectedCoin
        {
            get => _selectedCoin;
            set
            {
                _selectedCoin = value;
                OnPropertyChanged(nameof(SelectedCoin));
            }
        }

        private ObservableCollection<CoinDataViewModel> _currentCoins;
        public ObservableCollection<CoinDataViewModel> CurrentCoins
        {
            get => _currentCoins;
            set
            {
                _currentCoins = value;
                OnPropertyChanged(nameof(CurrentCoins));
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
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private ObservableCollection<CurrencyDataViewModel> _allCurrencies;
        public ObservableCollection<CurrencyDataViewModel> AllCurrencies
        {
            get => _allCurrencies;
            set
            {
                _allCurrencies = value;
                OnPropertyChanged(nameof(AllCurrencies));
            }
        }

        private CurrencyDataViewModel _selectedCurrency;
        public CurrencyDataViewModel SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }

        private CoinSearchDataViewModel _coinSearchDataViewModel;
        public CoinSearchDataViewModel CoinSearchDataViewModel
        {
            get => _coinSearchDataViewModel;
            set
            {
                _coinSearchDataViewModel = value;
                OnPropertyChanged(nameof(CoinSearchDataViewModel));
            }
        }

        private int _totalCoins;
        public int TotalCoins
        {
            get => _totalCoins;
            set
            {
                _totalCoins = value;
                OnPropertyChanged(nameof(TotalCoins));
                OnPropertyChanged(nameof(TotalCoinsText));
            }
        }
        public string TotalCoinsText => $"Number of coins: {_totalCoins}";
        public ICommand AddCoinCommand { get; set; }
        public ICommand UpdateCoinCommand { get; set; }
        public ICommand DeleteCoinCommand { get; set; }
        public CoinDisplayViewModel()
        {
            _coinService = App.AppHost.Services.GetRequiredService<ICoinService>();
            _countryService = App.AppHost.Services.GetRequiredService<ICountryService>();
            _currencyService = App.AppHost.Services.GetRequiredService<ICurrencyService>();
            CurrentCoins = new ObservableCollection<CoinDataViewModel>();
            AllCurrencies = new ObservableCollection<CurrencyDataViewModel>();
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            CoinSearchDataViewModel = new CoinSearchDataViewModel();
            PageNumber = 1;
            PageSize = GlobalParams.PAGE_SIZE;
            TotalPages = _coinService.GetTotalPageNumber(PageSize);

            AddCoinCommand = new RelayCommand(c => CreateCoin());
            DeleteCoinCommand = new RelayCommand(c => DeleteCoin());
            UpdateCoinCommand = new RelayCommand(c => UpdateCoin());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());
            SearchCommand = new RelayCommand(c => SearchCoins());
            RefreshSearchCommand = new RelayCommand(c => RefreshSearch());
            getAllCountries();
            getAllCurrencies();

            GetCoins(PageNumber, PageSize, CoinSearchDataViewModel);
        }

        private void getAllCountries()
        {
            AllCountries.Clear();
            foreach (var country in _countryService.GetAll())
            {
                AllCountries.Add(new CountryDataViewModel(country));
            }
        }

        private void getAllCurrencies()
        {
            AllCurrencies.Clear();
            foreach (var currency in _currencyService.GetAll())
            {
                AllCurrencies.Add(new CurrencyDataViewModel(currency));
            }
        }

        public override void GetNextPage()
        {
            if (PageNumber + 1 <= TotalPages)
            {
                PageNumber++;
                GetCoins(PageNumber, TotalPages, CoinSearchDataViewModel);
            }
        }

        public override void GetPreviousPage()
        {
            if (PageNumber - 1 > 0)
            {
                PageNumber--;
                GetCoins(PageNumber, PageSize, CoinSearchDataViewModel);
            }
        }

        public override void GetTotalItemsNumber()
        {
            TotalCoins = _coinService.GetTotalCoinsNumber();
        }


        private void SearchCoins()
        {
            PageNumber = 1;
            if (SelectedCurrency != null)
            {
                CoinSearchDataViewModel.Currency = SelectedCurrency;
            }
            if (SelectedCountry != null)
            {
                CoinSearchDataViewModel.Country = SelectedCountry;
            }
            GetCoins(PageNumber, PageSize, CoinSearchDataViewModel);
        }

        private void RefreshSearch()
        {
            PageNumber = 1;
            CoinSearchDataViewModel = new CoinSearchDataViewModel();
            SelectedCountry = null;
            SelectedCurrency = null;
            GetCoins(PageNumber, PageSize, CoinSearchDataViewModel);
        }

        public void CreateCoin()
        {
            CoinDetailsPage coinDetailsPage = new CoinDetailsPage(null);
            bool? result = coinDetailsPage.ShowDialog();
            if (result == true)
            {
                coinDetailsPage.Close();
                TotalPages = _coinService.GetTotalPageNumber(PageSize);
                GetCoins(PageNumber, PageSize, CoinSearchDataViewModel);
            }
        }

        private void DeleteCoin()
        {
            if (SelectedCoin != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected coin?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _coinService.Delete(SelectedCoin.ToCoinDTO());
                    TotalPages = _coinService.GetTotalPageNumber(PageSize);
                    GetCoins(PageNumber, PageSize, CoinSearchDataViewModel);
                    MessageBox.Show("You successfully deleted a coin.", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please, select coin you want to delete", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateCoin()
        {
            if (SelectedCoin == null)
            {
                MessageBox.Show("Please, select coin you want to update", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                CoinDetailsPage coinDetailsPage = new CoinDetailsPage(SelectedCoin);
                bool? result = coinDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetCoins(PageNumber, PageSize, CoinSearchDataViewModel);
                }
            }
        }

        private void GetCoins(int pageNumber, int pageSize, CoinSearchDataViewModel coinSearchDataViewModel)
        {
            CurrentCoins.Clear();
            var coins = _coinService.GetByPage(pageNumber-1, pageSize, coinSearchDataViewModel.ToCoinSearchDataDTO());
            foreach (var coin in coins)
            {
                CurrentCoins.Add(new CoinDataViewModel(coin));
            }
            GetTotalItemsNumber();
            OnPropertyChanged(nameof(CurrentCoins));
        }
    }
}
