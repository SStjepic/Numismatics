using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Numismatics.WPF.Util;

namespace Numismatics.WPF.ViewModel.CurrencyViewModel
{
    public class CurrencyCrudViewModel: INotifyPropertyChanged
    {
        private CurrencyService _currencyService;

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
            _currencyService = new CurrencyService();

            CurrentCurrency = currency != null ? currency : new CurrencyDataViewModel(null);
            _isUpdate = currency != null ? true : false;

            AddCurrencyCommand = new RelayCommand(c => CreateCurrency());
        }

        private bool CreateCurrency()
        {
            if (CurrentCurrency.IsValid)
            {
                if (_isUpdate)
                {
                    _currencyService.Update(CurrentCurrency.ToCurrencyDTO());
                    MessageBox.Show("You successfully update currency", "Excelent");
                }
                else
                {
                    _currencyService.Create(CurrentCurrency.ToCurrencyDTO());
                    MessageBox.Show("You successfully add new currency", "Excelent");
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
