using Numismatics.CORE.DTO;
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
            ShowCountriesCommand = new RelayCommand(c => ShowCountries());
        }

        private void ShowBanknotes()
        {
        }

        private void ShowCountries()
        {
            HomeCRUDView = new HomeCountryViewModel();
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
