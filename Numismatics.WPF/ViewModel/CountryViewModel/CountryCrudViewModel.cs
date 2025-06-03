using Numismatics.CORE.Services;
using Numismatics.WPF.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModel.CountryViewModel
{
    public class CountryCrudViewModel: INotifyPropertyChanged
    {
        private CountryService _countryService;

        private CountryDataViewModel _currentCountry;
        public CountryDataViewModel CurrentCountry
        {
            get => _currentCountry;
            set
            {
                _currentCountry = value;
                OnPropertyChanged(nameof(CurrentCountry));
            }
        }

        private bool _isUpdate;

        public ICommand AddCountryCommand { get; set; }
        public CountryCrudViewModel(CountryDataViewModel country) 
        {
            _countryService = new CountryService();

            CurrentCountry = country != null? country : new CountryDataViewModel(null);
            _isUpdate = country != null? true : false;

            AddCountryCommand = new RelayCommand(c => CreateCountry());
        }

        private bool CreateCountry()
        {
            if(CurrentCountry.IsValid)
            {
                if (_isUpdate)
                {
                    _countryService.Update(CurrentCountry.ToCountryDTO());
                    MessageBox.Show("You successfully update country", "Excelent", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _countryService.Create(CurrentCountry.ToCountryDTO());
                    MessageBox.Show("You successfully add new country", "Excelent", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)!.DialogResult = true;
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
