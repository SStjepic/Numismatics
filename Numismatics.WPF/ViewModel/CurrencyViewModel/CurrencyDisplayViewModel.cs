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

        public ICommand AddCurrencyCommand { get; set; }
        public ICommand UpdateCurrencyCommand { get; set; }
        public ICommand DeleteCurrencyCommand { get; set; }
        public CurrencyDisplayViewModel()
        {
            _currencyService = new CurrencyService();
            CurrentCurrencies = new ObservableCollection<CurrencyDataViewModel>();

            PageNumber = 1;
            PageSize = 10;
            TotalPages = _currencyService.GetTotalPageNumber(PageSize);

            AddCurrencyCommand = new RelayCommand(c => CreateCurrency());
            UpdateCurrencyCommand = new RelayCommand(c => UpdateCurrency());
            DeleteCurrencyCommand = new RelayCommand(c => DeleteCurrency());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());

            GetCurrencies(PageNumber-1, PageSize);
        }
        public override void GetNextPage()
        {
            if (PageNumber + 1 <= TotalPages)
            {
                PageNumber++;
                GetCurrencies(PageNumber, TotalPages);
            }
        }

        public override void GetPreviousPage()
        {
            if (PageNumber - 1 > 0)
            {
                PageNumber--;
                GetCurrencies(PageNumber, PageSize);
            }
        }

        public void CreateCurrency()
        {
            CurrencyDetailsPage currencyDetailsPage = new CurrencyDetailsPage(null);
            bool? result = currencyDetailsPage.ShowDialog();
            if (result == true)
            {
                currencyDetailsPage.Close();
                GetCurrencies(PageNumber, PageSize);
            }
        }

        private void DeleteCurrency()
        {
            if (SelectedCurrency != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected currency?", "Delete", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _currencyService.Delete(SelectedCurrency.ToCurrencyDTO());
                    GetCurrencies(PageNumber, PageSize);
                }
            }
            else
            {
                MessageBox.Show("Please, select currency you want to delete", "Delete");
            }
        }

        private void UpdateCurrency()
        {
            if (SelectedCurrency == null)
            {
                MessageBox.Show("Please, select currency you want to update", "Update");
            }
            else
            {
                CurrencyDetailsPage currencyDetailsPage = new CurrencyDetailsPage(SelectedCurrency);
                bool? result = currencyDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetCurrencies(PageNumber, PageSize);
                }
            }
        }

        private void GetCurrencies(int pageNumber, int pageSize)
        {
            CurrentCurrencies.Clear();
            var currencies = _currencyService.GetByPage(pageNumber, pageSize);
            foreach (var currency in currencies)
            {
                CurrentCurrencies.Add(new CurrencyDataViewModel(currency));
            }
            OnPropertyChanged(nameof(CurrentCurrencies));
        }
    }
}
