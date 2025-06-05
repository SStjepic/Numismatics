using Numismatics.WPF.View.CountryView;
using Numismatics.CORE.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using Numismatics.WPF.Utils;

namespace Numismatics.WPF.ViewModels.CountryViewModels
{
    public class CountryDisplayViewModel: DisplayViewMode
    {
        private CountryService _countryService;

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

        private ObservableCollection<CountryDataViewModel> _currentCountries;
        public ObservableCollection<CountryDataViewModel> CurrentCountries
        {
            get => _currentCountries;
            set
            {
                _currentCountries = value;
                OnPropertyChanged(nameof(CurrentCountries));
            }
        }

        private CountrySearchDataViewModel _countrySearchDataViewModel;
        public CountrySearchDataViewModel CountrySearchDataViewModel
        {
            get => _countrySearchDataViewModel;
            set 
            {
                _countrySearchDataViewModel = value;
                OnPropertyChanged(nameof(CountrySearchDataViewModel));
            }
        }

        private int _totalCountries;
        public int TotalCountries
        {
            get => _totalCountries;
            set
            {
                _totalCountries = value;
                OnPropertyChanged(nameof(TotalCountries));
                OnPropertyChanged(nameof(TotalCountriesText));
            }
        }
        public string TotalCountriesText => $"Number of countries: {_totalCountries}";

        public ICommand AddCountryCommand { get; set; }
        public ICommand UpdateCountryCommand { get; set; }
        public ICommand DeleteCountryCommand { get; set; }
        public CountryDisplayViewModel() 
        {
            _countryService = new CountryService();
            CurrentCountries = new ObservableCollection<CountryDataViewModel>();
            CountrySearchDataViewModel = new CountrySearchDataViewModel();

            PageNumber = 1;
            PageSize = GlobalParams.PAGE_SIZE;
            TotalPages = _countryService.GetTotalPageNumber(PageSize);

            AddCountryCommand = new RelayCommand(c => CreateCountry());
            DeleteCountryCommand = new RelayCommand(c => DeleteCountry());
            UpdateCountryCommand = new RelayCommand(c => UpdateCountry());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());
            SearchCommand = new RelayCommand(c => SearchCountries());
            RefreshSearchCommand = new RelayCommand(c => RefreshSearch());

            GetCountries(PageNumber, PageSize, CountrySearchDataViewModel);
        }

        public override void GetNextPage()
        {
            if (PageNumber + 1 <= TotalPages)
            {
                PageNumber++;
                GetCountries(PageNumber, TotalPages, CountrySearchDataViewModel);
            }
        }

        public override void GetPreviousPage()
        {
            if (PageNumber - 1 > 0)
            {
                PageNumber--;
                GetCountries(PageNumber, PageSize, CountrySearchDataViewModel);
            }
        }
        public override void GetTotalItemsNumber()
        {
            TotalCountries = _countryService.GetTotalCountriesNumber();
        }

        private void SearchCountries()
        {
            PageNumber = 1;
            GetCountries(PageNumber, PageSize, CountrySearchDataViewModel);
        }

        private void RefreshSearch()
        {
            PageNumber = 1;
            CountrySearchDataViewModel = new CountrySearchDataViewModel();
            GetCountries(PageNumber, PageSize, CountrySearchDataViewModel);
        }
        public void CreateCountry()
        {
            CountryDetailsPage countryDetailsPage = new CountryDetailsPage(null);
            bool? result = countryDetailsPage.ShowDialog();
            if(result == true)
            {
                countryDetailsPage.Close();
                TotalPages = _countryService.GetTotalPageNumber(PageSize);
                GetCountries(PageNumber, PageSize, CountrySearchDataViewModel);
            }
        }

        private void DeleteCountry()
        {
            if (SelectedCountry != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected country?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _countryService.Delete(SelectedCountry.ToCountryDTO());
                    TotalPages = _countryService.GetTotalPageNumber(PageSize);
                    GetCountries(PageNumber, PageSize, CountrySearchDataViewModel);
                    MessageBox.Show("You successfully deleted a country.", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please, select country you want to delete", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateCountry()
        {
            if (SelectedCountry == null)
            {
                MessageBox.Show("Please, select country you want to update", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                CountryDetailsPage countryDetailsPage = new CountryDetailsPage(SelectedCountry);
                bool? result = countryDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetCountries(PageNumber, PageSize, CountrySearchDataViewModel);
                }
            }
        }

        private void GetCountries(int pageNumber, int pageSize, CountrySearchDataViewModel countrySearchDataViewModel)
        {
            CurrentCountries.Clear();
            var countries = _countryService.GetByPage(pageNumber-1, pageSize, countrySearchDataViewModel.ToCountrySearchDataDTO());
            foreach (var country in countries)
            {
                CurrentCountries.Add(new CountryDataViewModel(country));
            }
            GetTotalItemsNumber();
            OnPropertyChanged(nameof(CurrentCountries));
        }
    }
}
