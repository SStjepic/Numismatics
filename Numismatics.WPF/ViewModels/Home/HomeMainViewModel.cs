using Numismatics.CORE.Services;
using Numismatics.WPF.ViewModels.CoinViewModel;
using Numismatics.WPF.ViewModels.CountryViewModel;
using Numismatics.WPF.ViewModels.CurrencyViewModel;
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
    public class HomeMainViewModel: INotifyPropertyChanged
    {
        public HomeMainViewModel() 
        {
            CurrentItems = new ObservableCollection<object>();
            ShowCountriesCommand = new RelayCommand(c => ShowCountries());
            ShowCurrenciesCommand = new RelayCommand(c => ShowCurrencies());
            ShowCoinsCommand = new RelayCommand(c => ShowCoins());
            ShowBanknotesCommand = new RelayCommand(c => ShowBanknotes());
            _pageNumber = 1;
            _pageSize = PageSizeOption[0];
            ShowBanknotesCommand.Execute(this);
        }
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

        private readonly List<int> _pageSizeOption = new List<int>() { 10, 25, 50 };
        public List<int> PageSizeOption
        {
            get { return _pageSizeOption; }
        }

        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                OnPropertyChanged(nameof(PageSize));
                GetByPage();
            }
        }
        private int _pageNumber;
        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                if (value < 1) value = 1;
                _pageNumber = value;
                OnPropertyChanged(nameof(PageNumber));
                GetByPage();
            }
        }
        public IHomeViewModel CurrentViewModel { get; set; }
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
        private void ShowBanknotes()
        {
            CurrentViewModel = new HomeBanknoteViewModel();
            SetCommands();
        }
        private void ShowCountries()
        {
            CurrentViewModel = new HomeCountryViewModel();
            SetCommands();
        }

        private void ShowCurrencies()
        {
            CurrentViewModel = new HomeCurrencyViewModel();
            SetCommands();
        }
        private void ShowCoins()
        {
            CurrentViewModel = new HomeCoinViewModel();
            SetCommands();
        }

        public void GetByPage()
        {
            CurrentItems.Clear();
            var items = CurrentViewModel.GetByPage(PageNumber, PageSize);
            foreach (var item in items)
            {
                CurrentItems.Add(item);
            }
        }
        public void GetNextPage()
        {
            if(CurrentItems.Count == PageSize)
            {
                PageNumber++;
            }
            
        }
        public void GetPrevPage()
        {
            if (PageNumber > 1)
            {
                PageNumber--;
            }
        }

        private void SetCommands()
        {
            AddItemCommand = new RelayCommand(a => AddItem());
            UpdateItemCommand = new RelayCommand(u => UpdateItem());
            DeleteItemCommand = new RelayCommand(d => DeleteItem());
            GetByPage();
        }
        private void AddItem()
        {
            var newItem = CurrentViewModel.Add();
            CurrentItems.Add(newItem);
        }

        private void UpdateItem()
        {

            var newItem = CurrentViewModel.Update(SelectedItem);
            CurrentItems.Remove(SelectedItem);
            CurrentItems.Add(newItem);
        }
        private void DeleteItem()
        {
            var oldItem = CurrentViewModel.Delete(SelectedItem);
            CurrentItems.Remove(oldItem);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
