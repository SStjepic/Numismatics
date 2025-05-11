using Microsoft.Win32;
using Numismatic.WPF.ViewModel.CountryViewModel;
using Numismatic.WPF.ViewModel.CurrencyViewModel;
using Numismatic.WPF.ViewModels;
using Numismatic.WPF.ViewModels.BanknoteViewModel;
using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Numismatic.WPF.ViewModel.BanknoteViewModel
{
    public class BanknoteCrudViewModel: INotifyPropertyChanged
    {
        private BanknoteService _banknoteService;
        private CountryService _countryService;
        private NationalCurrencyService _nationalCurrencyService;

        private BanknoteDataViewModel _currentBanknote;
        public BanknoteDataViewModel CurrentBanknote
        {
            get => _currentBanknote;

            set
            {
                _currentBanknote = value;
                OnPropertyChanged(nameof(CurrentBanknote));
            }
        }
        private ObservableCollection<CountryDataViewModel> _allCountries;
        public ObservableCollection<CountryDataViewModel> AllCountries
        {
            get => this._allCountries;
            set
            {
                this._allCountries = value;
                OnPropertyChanged(nameof(AllCountries));
            }
        }

        private CountryDataViewModel _selectedCountry;
        public CountryDataViewModel SelectedCountry
        {
            get => this._selectedCountry;
            set
            {
                this._selectedCountry = value;
                CurrentBanknote.Country = value;
                getCurrencies(value);
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private List<String> _currencyValue;
        public List<String> CurrencyValue
        {
            get => this._currencyValue;
            set
            {
                this._currencyValue = value;
                OnPropertyChanged(nameof(CurrencyValue));
            }
        }

        private String _selectedCurrencyValue;
        public String SelectedCurrencyValue
        {
            get => this._selectedCurrencyValue;
            set
            {
                this._selectedCurrencyValue = value;
                if (value.Equals(CurrentBanknote.Currency.HunderthPartName))
                {
                    CurrentBanknote.HundertPart = value;
                }
                else
                {
                    CurrentBanknote.HundertPart = "";
                }
                OnPropertyChanged(nameof(SelectedCurrencyValue));
            }
        }

        private ObservableCollection<CurrencyDataViewModel> _currencies;
        public ObservableCollection<CurrencyDataViewModel> Currencies
        {
            get => this._currencies;
            set
            {
                this._currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        private CurrencyDataViewModel _selectedCurrency;
        public CurrencyDataViewModel SelectedCurrency
        {
            get => this._selectedCurrency;
            set
            {
                this._selectedCurrency = value;
                CurrentBanknote.Currency = value;
                getCurrencyValue(value);
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }

        private bool _isUpdate;
        public ICommand CreateBanknoteCommand { get; set; }
        public ICommand AddBanknoteCommand { get; set; }
        public ICommand DeleteBanknoteCommand { get; set; }
        public ICommand AddObversePictureCommand { get; set; }
        public ICommand DeleteObversePictureCommand { get; set; }
        public ICommand AddReversePictureCommand { get; set; }
        public ICommand DeleteReversePictureCommand { get; set; }
        public BanknoteCrudViewModel(BanknoteDataViewModel banknote)
        {
            _banknoteService = new BanknoteService();
            _countryService = new CountryService();
            _nationalCurrencyService = new NationalCurrencyService();

            CurrentBanknote = banknote != null ? banknote : new BanknoteDataViewModel(null);
            _isUpdate = banknote != null ? true : false;
            AllCountries = new ObservableCollection<CountryDataViewModel>();
            Currencies = new ObservableCollection<CurrencyDataViewModel>();
            CurrencyValue = new List<string>();

            CreateBanknoteCommand = new RelayCommand(c => createBanknote());
            AddBanknoteCommand = new RelayCommand(c => addBanknote());
            DeleteBanknoteCommand = new RelayCommand(c => deleteBanknote());
            AddObversePictureCommand = new RelayCommand(c => addObversePicture());
            DeleteObversePictureCommand = new RelayCommand(c => deleteObversePicture());
            AddReversePictureCommand = new RelayCommand(c => addReversePicture());
            DeleteReversePictureCommand = new RelayCommand(c => deleteReversePicture());

            getAllCountries();
            setupUI();
        }
        private void setupUI()
        {
            if (_isUpdate)
            {
                SelectedCountry = AllCountries.FirstOrDefault(c => c.Id == CurrentBanknote.Country.Id);
                OnPropertyChanged(nameof(SelectedCountry));
                SelectedCurrency = Currencies.FirstOrDefault(c => c.Id == CurrentBanknote.Currency.Id);
                OnPropertyChanged(nameof(SelectedCurrency));
                if (CurrentBanknote.HundertPart == "")
                {
                    SelectedCurrencyValue = CurrencyValue.FirstOrDefault(cv => cv.Equals(CurrentBanknote.Currency.Name));
                }
                else
                {
                    SelectedCurrencyValue = CurrencyValue.FirstOrDefault(cv => cv.Equals(CurrentBanknote.Currency.HunderthPartName));
                }
                OnPropertyChanged(nameof(SelectedCurrencyValue));
            }
        }
        private bool createBanknote()
        {
            if (CurrentBanknote.IsValid)
            {
                if (_isUpdate)
                {
                    _banknoteService.Update(CurrentBanknote.ToBanknoteDTO());
                    MessageBox.Show("You successfully update banknote.", "Excelent");
                }
                else
                {
                    _banknoteService.Create(CurrentBanknote.ToBanknoteDTO());
                    MessageBox.Show("You successfully add new banknote.", "Excelent");
                }
                Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)!.DialogResult = true;
                return true;
            }
            return false;
        }

        private void addBanknote()
        {
            if (CurrentBanknote.CurrentBanknoteQuality != null)
            {
                if(CurrentBanknote.CurrentBanknoteCode == null)
                {
                    MessageBox.Show("Enter banknote code", "Error");
                    return;
                }
                MoneyQuality banknoteQuality = (MoneyQuality)Enum.Parse(typeof(MoneyQuality), CurrentBanknote.CurrentBanknoteQuality);
                QualityKeyValuePair<string, MoneyQuality> pair = CurrentBanknote.Banknotes.FirstOrDefault(p => p.Key.Equals(CurrentBanknote.CurrentBanknoteCode));
                if (pair == null)
                {
                    CurrentBanknote.Banknotes.Add(new QualityKeyValuePair<string, MoneyQuality>(CurrentBanknote.CurrentBanknoteCode, banknoteQuality));
                }
                else
                {
                    MessageBox.Show($"Already exist banknote with code: {CurrentBanknote.CurrentBanknoteCode}");
                }
            }
            else
            {
                MessageBox.Show("Choose banknote quality", "Error");
            }
        }

        private void deleteBanknote()
        {
            if (CurrentBanknote.CurentBanknotePair != null)
            {
                CurrentBanknote.Banknotes.Remove(CurrentBanknote.CurentBanknotePair);
            }
            else
            {
                MessageBox.Show("Please select coin <code,quality> pair", "Error");
            }
        }

        public void addObversePicture()
        {
            CurrentBanknote.ObversePicture = getFilePath();
        }

        private void deleteObversePicture()
        {
            CurrentBanknote.ObversePicture = "";
        }

        private void addReversePicture()
        {
            CurrentBanknote.ReversePicture = getFilePath();
        }

        private void deleteReversePicture()
        {
            CurrentBanknote.ReversePicture = "";
        }

        private string getFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            fileDialog.FilterIndex = 1;
            if (fileDialog.ShowDialog() == true)
            {
                return fileDialog.FileName;
            }
            return "";
        }
        private void getAllCountries()
        {
            foreach (var country in _countryService.GetAll())
            {
                AllCountries.Add(new CountryDataViewModel(country));
            }
        }

        private void getCurrencies(CountryDataViewModel country)
        {
            foreach (var currency in _nationalCurrencyService.GetCurrencies(country.Id))
            {
                Currencies.Add(new CurrencyDataViewModel(currency));
            }
        }

        private void getCurrencyValue(CurrencyDataViewModel currency)
        {
            CurrencyValue.Add(currency.Name);
            CurrencyValue.Add(currency.HunderthPartName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
