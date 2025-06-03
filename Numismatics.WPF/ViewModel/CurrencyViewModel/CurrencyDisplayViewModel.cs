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
using Numismatics.WPF.View.CurrencyView;
using Numismatics.WPF.Util;
using Numismatics.WPF.ViewModel.CoinViewModel;

namespace Numismatics.WPF.ViewModel.CurrencyViewModel
{
    public class CurrencyDisplayViewModel: DisplayViewMode
    {
        private CurrencyService _currencyService;

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

        private ObservableCollection<CurrencyDataViewModel> _currentCurrencies;
        public ObservableCollection<CurrencyDataViewModel> CurrentCurrencies
        {
            get => _currentCurrencies;
            set
            {
                _currentCurrencies = value;
                OnPropertyChanged(nameof(CurrentCurrencies));
            }
        }

        private CurrencySearchDataViewModel _currencySearchDataViewModel;

        public CurrencySearchDataViewModel CurrencySearchDataViewModel
        {
            get => _currencySearchDataViewModel;
            set
            {
                _currencySearchDataViewModel = value;
                OnPropertyChanged(nameof(CurrencySearchDataViewModel));
            }
        }

        private int _totalCurrencies;
        public int TotalCurrencies
        {
            get => _totalCurrencies;
            set
            {
                _totalCurrencies = value;
                OnPropertyChanged(nameof(TotalCurrencies));
                OnPropertyChanged(nameof(TotalCurrenciesText));
            }
        }
        public string TotalCurrenciesText => $"Number of currencies: {_totalCurrencies}";

        public ICommand AddCurrencyCommand { get; set; }
        public ICommand UpdateCurrencyCommand { get; set; }
        public ICommand DeleteCurrencyCommand { get; set; }
        public CurrencyDisplayViewModel()
        {
            _currencyService = new CurrencyService();
            CurrentCurrencies = new ObservableCollection<CurrencyDataViewModel>();
            CurrencySearchDataViewModel = new CurrencySearchDataViewModel();

            PageNumber = 1;
            PageSize = GlobalParams.PAGE_SIZE;
            TotalPages = _currencyService.GetTotalPageNumber(PageSize);

            AddCurrencyCommand = new RelayCommand(c => CreateCurrency());
            UpdateCurrencyCommand = new RelayCommand(c => UpdateCurrency());
            DeleteCurrencyCommand = new RelayCommand(c => DeleteCurrency());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());
            SearchCommand = new RelayCommand(c => SearchCurrencies());
            RefreshSearchCommand = new RelayCommand(c => RefreshSearch());

            GetCurrencies(PageNumber, PageSize, CurrencySearchDataViewModel);
        }
        public override void GetNextPage()
        {
            if (PageNumber + 1 <= TotalPages)
            {
                PageNumber++;
                GetCurrencies(PageNumber, TotalPages, CurrencySearchDataViewModel);
            }
        }

        public override void GetPreviousPage()
        {
            if (PageNumber - 1 > 0)
            {
                PageNumber--;
                GetCurrencies(PageNumber, PageSize, CurrencySearchDataViewModel);
            }
        }
        public override void GetTotalItemsNumber()
        {
            TotalCurrencies = this._currencyService.GetTotalCurrenciesNumber();
        }

        private void SearchCurrencies()
        {
            PageNumber = 1;
            this.GetCurrencies(PageNumber, PageSize, CurrencySearchDataViewModel);
        }

        private void RefreshSearch()
        {
            PageNumber = 1;
            CurrencySearchDataViewModel = new CurrencySearchDataViewModel();
            this.GetCurrencies(PageNumber, PageSize, CurrencySearchDataViewModel);
        }

        public void CreateCurrency()
        {
            CurrencyDetailsPage currencyDetailsPage = new CurrencyDetailsPage(null);
            bool? result = currencyDetailsPage.ShowDialog();
            if (result == true)
            {
                currencyDetailsPage.Close();
                TotalPages = _currencyService.GetTotalPageNumber(PageSize);
                GetCurrencies(PageNumber, PageSize, CurrencySearchDataViewModel);
            }
        }

        private void DeleteCurrency()
        {
            if (SelectedCurrency != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected currency?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _currencyService.Delete(SelectedCurrency.ToCurrencyDTO());
                    TotalPages = _currencyService.GetTotalPageNumber(PageSize);
                    GetCurrencies(PageNumber, PageSize, CurrencySearchDataViewModel);
                }
            }
            else
            {
                MessageBox.Show("Please, select currency you want to delete", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateCurrency()
        {
            if (SelectedCurrency == null)
            {
                MessageBox.Show("Please, select currency you want to update", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                CurrencyDetailsPage currencyDetailsPage = new CurrencyDetailsPage(SelectedCurrency);
                bool? result = currencyDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetCurrencies(PageNumber, PageSize, CurrencySearchDataViewModel);
                }
            }
        }

        private void GetCurrencies(int pageNumber, int pageSize, CurrencySearchDataViewModel currencySearchDataViewModel)
        {
            CurrentCurrencies.Clear();
            var currencies = _currencyService.GetByPage(pageNumber-1, pageSize, currencySearchDataViewModel.ToCurrencySearchDataDTO());
            foreach (var currency in currencies)
            {
                CurrentCurrencies.Add(new CurrencyDataViewModel(currency));
            }
            this.GetTotalItemsNumber();
            OnPropertyChanged(nameof(CurrentCurrencies));
        }
    }
}
