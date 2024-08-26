using Numismatics.WPF.ViewModels.Home.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numismatics.CORE.DTO;
using Numismatics.WPF.Views;
using Numismatics.WPF.ViewModels.CountryViewModel;
using System.Windows;
using Numismatics.CORE.Services;

namespace Numismatics.WPF.ViewModels.Home
{
    public class HomeCountryViewModel : IHomeCRUDView
    {
        private CountryService _countryService;

        public HomeCountryViewModel()
        {
            _countryService = new CountryService();
        }
        public object? Add()
        {
            CountryView countryView = new CountryView(null);
            countryView.ShowDialog();
            if (countryView.CurrentCountry.IsValid)
            {
                return countryView.CurrentCountry;
            }
            return null;
        }

        public object? Delete(object entity)
        {
            if(entity != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected country?","Delete" ,MessageBoxButton.YesNo);
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    var countryData = entity as CountryDataViewModel;
                    _countryService.Delete(countryData.ToCountryDTO());
                }
            }
            else
            {
                MessageBox.Show("Please, select country you want to delete", "Delete");
            }
            return entity;
        }

        public object? Update(object entity)
        {
            if(entity == null)
            {
                MessageBox.Show("Please, select country you want to update", "Update");
            }
            else
            {
                var countryData = entity as CountryDataViewModel;
                CountryView countryView = new CountryView(countryData.ToCountryDTO());
                countryView.Show();

                if (countryView.CurrentCountry.IsValid)
                {
                    return countryView.CurrentCountry;
                }
                return null;
            }
            return null;
        }
    }
}
