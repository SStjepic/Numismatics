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
        public DisplayViewMode DisplayViewModel { get; set; }
        public ICommand DisplayBanknotePageCommand { get; set; }
        public ICommand DisplayCoinPageCommand { get; set; }
        public ICommand DisplayCountryPageCommand { get; set; }
        public ICommand DisplayCurrencyPageCommand { get; set; }

        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }

        private int _pageNumber;
        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                _pageNumber = value;
                OnPropertyChanged(nameof(PageNumber));
            }
        }
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                OnPropertyChanged(nameof(PageSize));
            }
        }



        public MainNavigationViewModel()
        {
            DisplayBanknotePageCommand = new RelayCommand(c => SetBanknotePage());
            DisplayCoinPageCommand = new RelayCommand(c => SetCoinPage());
            DisplayCountryPageCommand = new RelayCommand(c => SetCountryPage());
            DisplayCurrencyPageCommand = new RelayCommand(c => SetCurrencyPage());

            this.SetBanknotePage();
        }

        private void SetBanknotePage()
        {
            CurrentPage = new BanknotePage();
            DisplayViewModel = (this.CurrentPage as BanknotePage).BanknoteDisplayViewModel;
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(DisplayViewModel));
        }

        private void SetCoinPage()
        {
            CurrentPage = new CoinPage();
            DisplayViewModel = (this.CurrentPage as CoinPage).CoinDisplayViewModel;
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(DisplayViewModel));
        }

        private void SetCountryPage()
        {
            CurrentPage = new CountryPage();
            DisplayViewModel = (this.CurrentPage as CountryPage).CountryDisplayViewModel;
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(DisplayViewModel));
        }

        private void SetCurrencyPage()
        {
            CurrentPage = new CurrencyPage();
            DisplayViewModel = (this.CurrentPage as CurrencyPage).CurrencyDisplayViewModel;
            OnPropertyChanged(nameof(CurrentPage));
            OnPropertyChanged(nameof(DisplayViewModel));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
