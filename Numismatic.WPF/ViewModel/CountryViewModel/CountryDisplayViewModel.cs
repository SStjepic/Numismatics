using Numismatic.WPF.View.CountryView;
using Numismatic.WPF.ViewModels;
using Numismatics.CORE.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Numismatic.WPF.ViewModel.CountryViewModel
{
    public class CountryDisplayViewModel: INotifyPropertyChanged
    {
        private CountryService _countryService;

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

        private ObservableCollection<CountryDataViewModel> _currentCountries;
        public ObservableCollection<CountryDataViewModel> CurrentCountries
        {
            get => this._currentCountries;
            set
            {
                this._currentCountries = value;
                OnPropertyChanged(nameof(CurrentCountries));
            }
        }

        private int _pageNumber;
        public int PageNumber
        {
            get => this._pageNumber;
            set
            {
                this._pageNumber = value;
                OnPropertyChanged(nameof(PageNumber));
            }
        }
        private int _pageSize;
        public int PageSize
        {
            get => this._pageSize;
            set
            {
                this._pageSize = value;
                OnPropertyChanged(nameof(PageSize));
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

            AddCountryCommand = new RelayCommand(c => CreateCountry());
            DeleteCountryCommand = new RelayCommand(c => DeleteCountry());
            UpdateCountryCommand = new RelayCommand(c => UpdateCountry());

            GetCountrise(PageNumber, PageSize);
        }

        public void CreateCountry()
        {
            CountryDetailsPage countryDetailsPage = new CountryDetailsPage(null);
            bool? result = countryDetailsPage.ShowDialog();
            if(result == true)
            {
                countryDetailsPage.Close();
                GetCountrise(PageNumber, PageSize);
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
                    GetCountrise(PageNumber, PageSize);
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
                    GetCountrise(PageNumber, PageSize);
                }
            }
        }

        private void GetCountrise(int pageNumber, int pageSize)
        {
            CurrentCountries.Clear();
            var countries = _countryService.GetByPage(pageNumber, pageSize);
            foreach (var country in countries)
            {
                CurrentCountries.Add(new CountryDataViewModel(country));
            }
            OnPropertyChanged(nameof(CurrentCountries));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
