using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using Numismatics.WPF.ViewModels.BanknoteViewModel;
using Numismatics.WPF.ViewModels.CoinViewModel;
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
    public class HomeBanknoteViewModel : IHomeCRUDView
    {
        private BanknoteService _banknoteService;

        public HomeBanknoteViewModel()
        {
            _banknoteService = new BanknoteService();   
        }
        public object? Add()
        {
            BanknoteWindow banknoteWindow = new BanknoteWindow(null);
            banknoteWindow.ShowDialog();
            return null;
        }

        public object? Delete(object entity)
        {
            if (entity != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected banknote?", "Delete", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var banknoteData = entity as BanknoteDataViewModel;
                    _banknoteService.Delete(banknoteData.ToBanknoteDTO());
                    return entity;
                }
                return null;
            }
            else
            {
                MessageBox.Show("Please, select banknote you want to delete", "Delete");
                return null;
            }
        }

        public object? Update(object entity)
        {
            if (entity == null)
            {
                MessageBox.Show("Please, select banknote you want to update", "Update");
                return null;
            }
            else
            {
                var banknoteData = entity as BanknoteDataViewModel;
                BanknoteWindow banknoteWindow = new BanknoteWindow(banknoteData.ToBanknoteDTO());
                banknoteWindow.Show();

                if (banknoteWindow.CurrentBanknote.IsValid)
                {
                    return banknoteWindow.CurrentBanknote;
                }
                return null;
            }
        }
    }
}
