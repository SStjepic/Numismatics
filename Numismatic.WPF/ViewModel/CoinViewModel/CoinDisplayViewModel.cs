using Numismatic.WPF.View.CountryView;
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
using Numismatic.WPF.ViewModels;
using Numismatic.WPF.View.CoinView;

namespace Numismatic.WPF.ViewModel.CoinViewModel
{
    public class CoinDisplayViewModel: INotifyPropertyChanged
    {
        private CoinService _coinService;

        private CoinDataViewModel _selectedCoin;
        public CoinDataViewModel SelectedCoin
        {
            get => this._selectedCoin;
            set
            {
                this._selectedCoin = value;
                OnPropertyChanged(nameof(SelectedCoin));
            }
        }

        private ObservableCollection<CoinDataViewModel> _currentCoins;
        public ObservableCollection<CoinDataViewModel> CurrentCoins
        {
            get => this._currentCoins;
            set
            {
                this._currentCoins = value;
                OnPropertyChanged(nameof(CurrentCoins));
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

        public ICommand AddCoinCommand { get; set; }
        public ICommand UpdateCoinCommand { get; set; }
        public ICommand DeleteCoinCommand { get; set; }
        public CoinDisplayViewModel()
        {
            _coinService = new CoinService();
            CurrentCoins = new ObservableCollection<CoinDataViewModel>();

            PageNumber = 1;
            PageSize = 10;

            AddCoinCommand = new RelayCommand(c => CreateCoin());
            DeleteCoinCommand = new RelayCommand(c => DeleteCoin());
            UpdateCoinCommand = new RelayCommand(c => UpdateCoin());

            GetCoins(PageNumber, PageSize);
        }

        public void CreateCoin()
        {
            CoinDetailsPage coinDetailsPage = new CoinDetailsPage(null);
            bool? result = coinDetailsPage.ShowDialog();
            if (result == true)
            {
                coinDetailsPage.Close();
                GetCoins(PageNumber, PageSize);
            }
        }

        private void DeleteCoin()
        {
            if (SelectedCoin != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected coin?", "Delete", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _coinService.Delete(SelectedCoin.ToCoinDTO());
                    GetCoins(PageNumber, PageSize);
                }
            }
            else
            {
                MessageBox.Show("Please, select coin you want to delete", "Delete");
            }
        }

        private void UpdateCoin()
        {
            if (SelectedCoin == null)
            {
                MessageBox.Show("Please, select coin you want to update", "Update");
            }
            else
            {
                CoinDetailsPage coinDetailsPage = new CoinDetailsPage(SelectedCoin);
                bool? result = coinDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetCoins(PageNumber, PageSize);
                }
            }
        }

        private void GetCoins(int pageNumber, int pageSize)
        {
            CurrentCoins.Clear();
            var coins = _coinService.GetByPage(pageNumber, pageSize);
            foreach (var coin in coins)
            {
                CurrentCoins.Add(new CoinDataViewModel(coin));
            }
            OnPropertyChanged(nameof(CurrentCoins));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
