using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using Numismatics.WPF.Utils;
using Microsoft.Extensions.DependencyInjection;
using Numismatics.CORE.Services.Interface;

namespace Numismatics.WPF.ViewModels.CurrencyViewModels
{
    public class CurrencyCrudViewModel: INotifyPropertyChanged
    {
        private ICurrencyService _currencyService;

        private CurrencyDataViewModel _currentCurrency;
        public CurrencyDataViewModel CurrentCurrency
        {
            get => _currentCurrency;
            set
            {
                _currentCurrency = value;
                OnPropertyChanged(nameof(CurrentCurrency));
            }
        }

        private bool _isUpdate;

        public ICommand AddCurrencyCommand { get; set; }
        public CurrencyCrudViewModel(CurrencyDataViewModel currency)
        {
            _currencyService = App.AppHost.Services.GetRequiredService<ICurrencyService>();

            CurrentCurrency = currency != null ? currency : new CurrencyDataViewModel(null);
            _isUpdate = currency != null ? true : false;
            if (!_isUpdate)
            {
                CurrentCurrency.SubunitToMainUnit = 100;
            }
            AddCurrencyCommand = new RelayCommand(c => CreateCurrency());
        }

        private bool CreateCurrency()
        {
            if (CurrentCurrency.IsValid)
            {
                if (_isUpdate)
                {
                    var updatedCoin = _currencyService.Update(CurrentCurrency.ToCurrencyDTO());
                    CurrentCurrency = new CurrencyDataViewModel(updatedCoin);
                    MessageBox.Show("You successfully update currency", "Excelent", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var newCoin = _currencyService.Create(CurrentCurrency.ToCurrencyDTO());
                    CurrentCurrency = new CurrencyDataViewModel(newCoin);
                    MessageBox.Show("You successfully add new currency", "Excelent", MessageBoxButton.OK, MessageBoxImage.Information);
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
