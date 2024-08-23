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

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
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

        private ICommand _updateItemCommand;
        public ICommand UpdateItemCommand
        {
            get { return _updateItemCommand; }
            set
            {
                _updateItemCommand = value;
                OnPropertyChanged(nameof(UpdateItemCommand));
            }
        }

        private ICommand _deleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get { return _deleteItemCommand; }
            set
            {
                _deleteItemCommand = value;
                OnPropertyChanged(nameof(DeleteItemCommand));
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
            UpdateItemCommand = new RelayCommand(u => UpdateItem());
            DeleteItemCommand = new RelayCommand(d => DeleteItem());

        }
        private void AddItem()
        {
            HomeCRUDView.Add();
        }

        private void UpdateItem()
        {
            HomeCRUDView.Update(SelectedItem);
        }
        private void DeleteItem()
        {
            HomeCRUDView.Delete(SelectedItem);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
