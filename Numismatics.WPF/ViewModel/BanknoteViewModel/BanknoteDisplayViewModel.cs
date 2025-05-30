using Numismatics.WPF.View.BanknoteView;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Numismatics.WPF.Util;
using System.Security.Policy;
using Numismatics.WPF.ViewModel.CountryViewModel;
using Numismatics.WPF.ViewModel.CurrencyViewModel;

namespace Numismatics.WPF.ViewModel.BanknoteViewModel
{
    public class BanknoteDisplayViewModel : DisplayViewMode
    {
        
        private readonly BanknoteService _banknoteService;
        private readonly CountryService _countryService;
        private readonly CurrencyService _currencyService;

        private ObservableCollection<BanknoteDataViewModel> _currentBanknotes;
        public ObservableCollection<BanknoteDataViewModel> CurrentBanknotes 
        {
            get => _currentBanknotes;
            set
            {
                _currentBanknotes = value;
                OnPropertyChanged(nameof(CurrentBanknotes));
            }
        }

        private BanknoteDataViewModel _selectedBanknote;
        public BanknoteDataViewModel SelectedBanknote
        {
            get => _selectedBanknote;
            set
            {
                _selectedBanknote = value;
                OnPropertyChanged(nameof(SelectedBanknote));
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

        private BanknoteSearchDataViewModel _banknoteSearchDataViewModel;
        public BanknoteSearchDataViewModel BanknoteSearchDataViewModel 
        {
            get => _banknoteSearchDataViewModel;
            set
            {
                _banknoteSearchDataViewModel = value;
                OnPropertyChanged(nameof(BanknoteSearchDataViewModel));
            }
        }

        private int _totalBanknotes;
        public int TotalBanknotes
        {
            get => _totalBanknotes;
            set
            {
                _totalBanknotes = value;
                OnPropertyChanged(nameof(TotalBanknotes));
                OnPropertyChanged(nameof(TotalBanknotesText));
            }
        }
        public string TotalBanknotesText => $"Number of banknotes: {_totalBanknotes}";

        public ICommand AddBanknoteCommand {  get; set; }
        public ICommand UpdateBanknoteCommand { get; set; }
        public ICommand DeleteBanknoteCommand { get; set; }

        public BanknoteDisplayViewModel()
        {
            _banknoteService = new BanknoteService();
            _countryService = new CountryService();
            _currencyService = new CurrencyService();
            CurrentBanknotes = new ObservableCollection<BanknoteDataViewModel>();
            AllCurrencies = new ObservableCollection<CurrencyDataViewModel>();
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            BanknoteSearchDataViewModel = new BanknoteSearchDataViewModel();
            PageNumber = 1;
            PageSize = 10;
            TotalPages = _banknoteService.GetTotalPageNumber(PageSize);

            AddBanknoteCommand = new RelayCommand(c => CreateBanknote());
            UpdateBanknoteCommand = new RelayCommand(c => UpdateBanknote());
            DeleteBanknoteCommand = new RelayCommand(c => DeleteBanknote());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());
            SearchCommand = new RelayCommand(c => SearchBankotes());
            RefreshSearchCommand = new RelayCommand(c => RefreshSearch());
            getAllCountries();
            getAllCurrencies();

            GetBanknotes(PageNumber, PageSize, BanknoteSearchDataViewModel);
        }

        private void getAllCountries()
        {
            AllCountries.Clear();
            foreach(var country in _countryService.GetAll())
            {
                AllCountries.Add(new CountryDataViewModel(country));
            }
        }

        private void getAllCurrencies()
        {
            AllCurrencies.Clear();
            foreach(var currency in _currencyService.GetAll())
            {
                AllCurrencies.Add(new CurrencyDataViewModel(currency));
            }
        }

        
        public override void GetNextPage()
        {
            if (PageNumber+1 <= TotalPages) 
            {
                PageNumber++;
                GetBanknotes(PageNumber, TotalPages, BanknoteSearchDataViewModel);
            }
        }

        public override void GetPreviousPage()
        {
            if(PageNumber - 1 > 0)
            {
                PageNumber--;
                GetBanknotes(PageNumber, PageSize, BanknoteSearchDataViewModel);
            }
        }

        public override void GetTotalItemsNumber()
        {
            TotalBanknotes = this._banknoteService.GetTotalBanknotesNumber();
        }

        private void SearchBankotes()
        {
            PageNumber = 1;
            if(SelectedCurrency!= null)
            {
                BanknoteSearchDataViewModel.Currency = SelectedCurrency;
            }
            if (SelectedCountry != null) 
            {
                BanknoteSearchDataViewModel.Country = SelectedCountry;
            }
            this.GetBanknotes(PageNumber, PageSize, BanknoteSearchDataViewModel);
        }

        private void RefreshSearch()
        {
            PageNumber = 1;
            BanknoteSearchDataViewModel = new BanknoteSearchDataViewModel();
            SelectedCountry = null;
            SelectedCurrency = null;
            this.GetBanknotes(PageNumber, PageSize, BanknoteSearchDataViewModel);
        }

        private void GetBanknotes(int pageNumber, int pageSize, BanknoteSearchDataViewModel banknoteSearchData)
        {

            CurrentBanknotes.Clear();
            foreach (BanknoteDTO banknoteDTO in _banknoteService.GetByPage(pageNumber-1, pageSize, banknoteSearchData.ToBanknoteSearchDataDTO()))
            {
                CurrentBanknotes.Add(new BanknoteDataViewModel(banknoteDTO));
            }
            this.GetTotalItemsNumber();
            OnPropertyChanged(nameof(CurrentBanknotes));
        }

        private void CreateBanknote()
        {
            BanknoteDetailsPage banknoteDetailsPage = new BanknoteDetailsPage(null);
            bool? result = banknoteDetailsPage.ShowDialog();
            if(result == true)
            {
                TotalPages = _banknoteService.GetTotalPageNumber(PageSize);
                GetBanknotes(PageNumber, PageSize, BanknoteSearchDataViewModel);
            }

        }

        private void DeleteBanknote()
        {
            if (SelectedBanknote != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected banknote?", "Delete", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _banknoteService.Delete(SelectedBanknote.ToBanknoteDTO());
                    TotalPages = _banknoteService.GetTotalPageNumber(PageSize);
                    GetBanknotes(PageNumber, PageSize, BanknoteSearchDataViewModel);
                }
            }
            else
            {
                MessageBox.Show("Please, select banknote you want to delete", "Delete");
            }
        }

        private void UpdateBanknote()
        {
            if (SelectedBanknote == null)
            {
                MessageBox.Show("Please, select banknote you want to update", "Update");
            }
            else
            {
                BanknoteDetailsPage banknoteDetailsPage = new BanknoteDetailsPage(SelectedBanknote);
                bool? result = banknoteDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetBanknotes(PageNumber, PageSize, BanknoteSearchDataViewModel);
                }
            }
        }

    }
}
