using Numismatics.WPF.View.CountryView;
using Numismatics.CORE.Services;
using Numismatics.WPF.Util;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModel.CountryViewModel
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

        public ICommand AddCountryCommand { get; set; }
        public ICommand UpdateCountryCommand { get; set; }
        public ICommand DeleteCountryCommand { get; set; }
        public CountryDisplayViewModel() 
        {
            _countryService = new CountryService();
            CurrentCountries = new ObservableCollection<CountryDataViewModel>();

            PageNumber = 1;
            PageSize = 10;
            TotalPages = _countryService.GetTotalPageNumber(PageSize);

            AddCountryCommand = new RelayCommand(c => CreateCountry());
            DeleteCountryCommand = new RelayCommand(c => DeleteCountry());
            UpdateCountryCommand = new RelayCommand(c => UpdateCountry());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());

            GetCountries(PageNumber-1, PageSize);
        }

        public override void GetNextPage()
        {
            if (PageNumber + 1 <= TotalPages)
            {
                PageNumber++;
                GetCountries(PageNumber, TotalPages);
            }
        }

        public override void GetPreviousPage()
        {
            if (PageNumber - 1 > 0)
            {
                PageNumber--;
                GetCountries(PageNumber, PageSize);
            }
        }


        public void CreateCountry()
        {
            CountryDetailsPage countryDetailsPage = new CountryDetailsPage(null);
            bool? result = countryDetailsPage.ShowDialog();
            if(result == true)
            {
                countryDetailsPage.Close();
                GetCountries(PageNumber, PageSize);
            }
        }

        private void DeleteCountry()
        {
            if (SelectedCountry != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected country?", "Delete", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _countryService.Delete(SelectedCountry.ToCountryDTO());
                    GetCountries(PageNumber, PageSize);
                }
            }
            else
            {
                MessageBox.Show("Please, select country you want to delete", "Delete");
            }
        }

        private void UpdateCountry()
        {
            if (SelectedCountry == null)
            {
                MessageBox.Show("Please, select country you want to update", "Update");
            }
            else
            {
                CountryDetailsPage countryDetailsPage = new CountryDetailsPage(SelectedCountry);
                bool? result = countryDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetCountries(PageNumber, PageSize);
                }
            }
        }

        private void GetCountries(int pageNumber, int pageSize)
        {
            CurrentCountries.Clear();
            var countries = _countryService.GetByPage(pageNumber, pageSize);
            foreach (var country in countries)
            {
                CurrentCountries.Add(new CountryDataViewModel(country));
            }
            OnPropertyChanged(nameof(CurrentCountries));
        }
    }
}
