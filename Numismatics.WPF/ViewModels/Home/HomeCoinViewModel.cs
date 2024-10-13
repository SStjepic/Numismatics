using Numismatics.CORE.Services;
using Numismatics.WPF.ViewModels.BanknoteViewModel;
using Numismatics.WPF.ViewModels.CoinViewModel;
using Numismatics.WPF.ViewModels.CountryViewModel;
using Numismatics.WPF.ViewModels.Home.Interfaces;
using Numismatics.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.WPF.ViewModels.Home
{
    public class HomeCoinViewModel : IHomeViewModel
    {
        private CoinService _coinService;
        public HomeCoinViewModel() 
        {
            _coinService = new CoinService();
        }
        public object? Add()
        {
            CoinWindow coinWindow = new CoinWindow(null);
            coinWindow.ShowDialog();
            return null;
        }

        public object? Delete(object entity)
        {
            if (entity != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected coin?", "Delete", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var coinData = entity as CoinDataViewModel;
                    _coinService.Delete(coinData.ToCoinDTO());
                    return entity;
                }
                return null;
            }
            else
            {
                MessageBox.Show("Please, select coin you want to delete", "Delete");
                return null;
            }
        }

        public List<object> GetByPage(int pageNumber, int pageSize)
        {
            var coinDTOs = new List<object>();
            var coins = _coinService.GetByPage(pageNumber, pageSize);
            foreach (var coin in coins)
            {
                coinDTOs.Add(new CoinDataViewModel(coin));
            }

            return coinDTOs;
        }

        public object? Update(object entity)
        {
            if (entity == null)
            {
                MessageBox.Show("Please, select coin you want to update", "Update");
                return null;
            }
            else
            {
                var coinData = entity as CoinDataViewModel;
                CoinWindow coinView = new CoinWindow(coinData.ToCoinDTO());
                coinView.Show();

                if (coinView.CurrentCoin.IsValid)
                {
                    return coinView.CurrentCoin;
                }
                return null;
            }
        }
    }
}
