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
    public class HomeCoinViewModel : IHomeCRUDView
    {
        public object? Add()
        {
            CoinWindow coinWindow = new CoinWindow(null);
            coinWindow.ShowDialog();
            return null;
        }

        public object? Delete(object entity)
        {
            throw new NotImplementedException();
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
