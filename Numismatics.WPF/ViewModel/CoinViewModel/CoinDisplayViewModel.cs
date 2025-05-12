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
using Numismatics.WPF.View.CoinView;
using Numismatics.WPF.Util;

namespace Numismatics.WPF.ViewModel.CoinViewModel
{
    public class CoinDisplayViewModel:DisplayViewMode
    {
        private CoinService _coinService;

        private CoinDataViewModel _selectedCoin;
        public CoinDataViewModel SelectedCoin
        {
            get => _selectedCoin;
            set
            {
                _selectedCoin = value;
                OnPropertyChanged(nameof(SelectedCoin));
            }
        }

        private ObservableCollection<CoinDataViewModel> _currentCoins;
        public ObservableCollection<CoinDataViewModel> CurrentCoins
        {
            get => _currentCoins;
            set
            {
                _currentCoins = value;
                OnPropertyChanged(nameof(CurrentCoins));
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
            TotalPages = _coinService.GetTotalPageNumber(PageSize);

            AddCoinCommand = new RelayCommand(c => CreateCoin());
            DeleteCoinCommand = new RelayCommand(c => DeleteCoin());
            UpdateCoinCommand = new RelayCommand(c => UpdateCoin());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());

            GetCoins(PageNumber-1, PageSize);
        }
        public override void GetNextPage()
        {
            if (PageNumber + 1 <= TotalPages)
            {
                PageNumber++;
                GetCoins(PageNumber, TotalPages);
            }
        }

        public override void GetPreviousPage()
        {
            if (PageNumber - 1 > 0)
            {
                PageNumber--;
                GetCoins(PageNumber, PageSize);
            }
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
