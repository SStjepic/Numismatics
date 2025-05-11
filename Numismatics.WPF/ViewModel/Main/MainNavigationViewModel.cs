using Numismatics.WPF.View;
using Numismatics.WPF.View.CoinView;
using Numismatics.WPF.View.CountryView;
using Numismatics.WPF.View.CurrencyView;
using Numismatics.WPF.Util;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModel.Main
{
    public class MainNavigationViewModel: INotifyPropertyChanged
    {
        public Page CurrentPage {  get; set; }
        public ICommand DisplayBanknotePageCommand { get; set; }
        public ICommand DisplayCoinPageCommand { get; set; }
        public ICommand DisplayCountryPageCommand { get; set; }
        public ICommand DisplayCurrencyPageCommand { get; set; }

        public MainNavigationViewModel()
        {
            DisplayBanknotePageCommand = new RelayCommand(c => SetBanknotePage());
            DisplayCoinPageCommand = new RelayCommand(c => SetCoinPage());
            DisplayCountryPageCommand = new RelayCommand(c => SetCountryPage());
            DisplayCurrencyPageCommand = new RelayCommand(c => SetCurrencyPage());
        }

        private void SetBanknotePage()
        {
            CurrentPage = new BanknotePage();
            OnPropertyChanged(nameof(CurrentPage));
        }

        private void SetCoinPage()
        {
            CurrentPage = new CoinPage();
            OnPropertyChanged(nameof(CurrentPage));
        }

        private void SetCountryPage()
        {
            CurrentPage = new CountryPage();
            OnPropertyChanged(nameof(CurrentPage));
        }

        private void SetCurrencyPage()
        {
            CurrentPage = new CurrencyPage();
            OnPropertyChanged(nameof(CurrentPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
