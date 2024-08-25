using Numismatics.CORE.Services;
using Numismatics.WPF.ViewModels.CountryViewModel;
using Numismatics.WPF.ViewModels.CurrencyViewModel;
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
    public class HomeCurrencyViewModel : IHomeCRUDView
    {
        private CurrencyService _currencyService;
        public HomeCurrencyViewModel()
        {
            _currencyService = new CurrencyService();
        }
        public object Add()
        {
            CurrencyWindow currencyWindow = new CurrencyWindow(null);
            currencyWindow.Show();
            if (currencyWindow.CurrentCurrency.IsValid)
            {
                return currencyWindow.CurrentCurrency;
            }
            return null;
        }

        public object Delete(object entity)
        {
            throw new NotImplementedException();
        }

        public object Update(object entity)
        {
            if (entity == null)
            {
                MessageBox.Show("Please, select currency you want to update", "Update");
            }
            else
            {
                var currencyData = entity as CurrencyDataViewModel;
                CurrencyWindow currencyWindow = new CurrencyWindow(currencyData.ToCurrencyDTO());
                currencyWindow.Show();
                if (currencyWindow.CurrentCurrency.IsValid)
                {
                    return currencyWindow.CurrentCurrency;
                }
                return null;
            }
            return null;
        }
    }
}
