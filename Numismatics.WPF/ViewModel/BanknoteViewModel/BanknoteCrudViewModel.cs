using Microsoft.Win32;
using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.Services;
using Numismatics.WPF.Util;
using Numismatics.WPF.ViewModel.CountryViewModel;
using Numismatics.WPF.ViewModel.CurrencyViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModel.BanknoteViewModel
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
            get => _allCountries;
            set
            {
                _allCountries = value;
                OnPropertyChanged(nameof(AllCountries));
            }
        }

        private CountryDataViewModel _selectedCountry;
        public CountryDataViewModel SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                CurrentBanknote.Country = value;
                getCurrencies(value);
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        private ObservableCollection<string> _currencyUnitNames;
        public ObservableCollection<string> CurrencyUnitNames
        {
            get => _currencyUnitNames;
            set
            {
                _currencyUnitNames = value;
                OnPropertyChanged(nameof(CurrencyUnitNames));
            }
        }

        private string _selectedCurrencyUnitName;
        public string SelectedCurrencyUnitName
        {
            get => _selectedCurrencyUnitName;
            set
            {
                _selectedCurrencyUnitName = value;
                if (value.Equals(CurrentBanknote.Currency.SubunitName))
                {
                    CurrentBanknote.SubunitString = value;
                }
                else
                {
                    CurrentBanknote.SubunitString = "";
                }
                OnPropertyChanged(nameof(SelectedCurrencyUnitName));
            }
        }

        private ObservableCollection<CurrencyDataViewModel> _currencies;
        public ObservableCollection<CurrencyDataViewModel> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }

        private CurrencyDataViewModel _selectedCurrency;
        public CurrencyDataViewModel SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                CurrentBanknote.Currency = value;
                getCurrencyValue(value);
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }
        public string NumberOfBanknotes => $"Number of banknotes: {CurrentBanknote.Banknotes.Count}";

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
            CurrencyUnitNames = new ObservableCollection<string>();

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
                if (CurrentBanknote.SubunitString == "")
                {
                    SelectedCurrencyUnitName = CurrencyUnitNames.FirstOrDefault(cv => cv.Equals(CurrentBanknote.Currency.MainUnitName));
                }
                else
                {
                    SelectedCurrencyUnitName = CurrencyUnitNames.FirstOrDefault(cv => cv.Equals(CurrentBanknote.Currency.SubunitName));
                }
                OnPropertyChanged(nameof(SelectedCurrencyUnitName));
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
                    OnPropertyChanged(nameof(NumberOfBanknotes));
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
                OnPropertyChanged(nameof(NumberOfBanknotes));
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
            Currencies.Clear();
            foreach (var currency in _nationalCurrencyService.GetCurrencies(country.Id))
            {
                Currencies.Add(new CurrencyDataViewModel(currency));
            }
        }

        private void getCurrencyValue(CurrencyDataViewModel currency)
        {
            CurrencyUnitNames.Clear();
            CurrencyUnitNames.Add(currency.MainUnitName);
            CurrencyUnitNames.Add(currency.SubunitName);
            OnPropertyChanged(nameof(CurrencyUnitNames));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
