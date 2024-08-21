using Numismatics.CORE.DTO;
using Numismatics.CORE.Services.CountryService;
using Numismatics.WPF.ViewModels.CountryViewModel;
using Numismatics.WPF.ViewModels.Home.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModels.Home
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private CountryService _countryService;
        public IHomeCRUDView HomeCRUDView { get; set; }
        public ICommand ShowBanknotesCommand { get; set; }
        public ICommand ShowCoinsCommand { get; set; }
        public ICommand ShowCountriesCommand { get; set; }
        public ICommand ShowCurrenciesCommand { get; set; }

        private ICommand _addItemCommand;
        public ICommand AddItemCommand 
        {
            get { return _addItemCommand; }
            set
            {
                _addItemCommand = value;
                OnPropertyChanged(nameof(AddItemCommand));
            }
        }

        private ObservableCollection<object> _currentItems;
        public ObservableCollection<object> CurrentItems
        {
            get { return _currentItems; }
            set
            {
                _currentItems = value;
                OnPropertyChanged(nameof(CurrentItems));

            }
        }


        public HomePageViewModel()
        {
            CurrentItems = new ObservableCollection<object>();
            ShowCountriesCommand = new RelayCommand(c => ShowCountries());
            _countryService = new CountryService();

        }

        private void ShowBanknotes()
        {
        }

        private void ShowCountries()
        {
            HomeCRUDView = new HomeCountryViewModel();
            CurrentItems.Clear();
            var countries = _countryService.GetAll();
            foreach (var country in countries)
            {
                CurrentItems.Add(new CountryDataViewModel(country));
            }
            SetCommands();
        }

        private void SetCommands()
        {
            AddItemCommand = new RelayCommand(a => AddItem());

        }
        private void AddItem()
        {
            HomeCRUDView.Add();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
