using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using Numismatics.WPF.Util;
using Numismatics.WPF.ViewModel.CountryViewModel;
using Numismatics.WPF.ViewModel.CurrencyViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.WPF.ViewModel.CoinViewModel
{
    public class CoinDataViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        public long Id { get; set; }

        private CountryDataViewModel _country;
        public CountryDataViewModel Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        private CurrencyDataViewModel _currency;
        public CurrencyDataViewModel Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        private string _subunitString;
        public string SubunitString
        {
            get { return _subunitString; }
            set
            {
                _subunitString = value;
                OnPropertyChanged(nameof(SubunitString));
            }
        }

        private string _year;
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        private Era _era;
        public Era Era
        {
            get { return _era; }
            set
            {
                _era = value;
                OnPropertyChanged(nameof(Era));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _obversePicture;
        public string ObversePicture
        {
            get
            {
                return _obversePicture;
            }
            set
            {
                _obversePicture = value;
                OnPropertyChanged(nameof(ObversePicture));
            }
        }

        private string _reversePicture;
        public string ReversePicture
        {
            get
            {
                return _reversePicture;
            }
            set
            {
                _reversePicture = value;
                OnPropertyChanged(nameof(ReversePicture));
            }
        }
        private ObservableCollection<QualityKeyValuePair<MoneyQuality, int>> _coins;
        public ObservableCollection<QualityKeyValuePair<MoneyQuality, int>> Coins
        {
            get { return _coins; }
            set
            {
                _coins = value;
                OnPropertyChanged(nameof(Coins));
            }
        }
        public string CurrentCoinQuality { get; set; }
        private QualityKeyValuePair<MoneyQuality, int> _currentCoinQualityPair;
        public QualityKeyValuePair<MoneyQuality, int> CurrentCoinQualityPair
        {
            get { return _currentCoinQualityPair; }
            set
            {
                _currentCoinQualityPair = value;
                OnPropertyChanged(nameof(CurrentCoinQualityPair));
            }
        }

        public CoinDataViewModel() { }
        public CoinDataViewModel(CoinDTO coin)
        {
            Coins = new ObservableCollection<QualityKeyValuePair<MoneyQuality, int>>();
            if (coin != null)
            {
                Id = coin.Id;
                Country = new CountryDataViewModel(coin.Country);
                Currency = new CurrencyDataViewModel(coin.Currency);
                Year = coin.IssueDate.Year != 0? coin.IssueDate.Year.ToString(): "";
                Era = coin.IssueDate.Era;
                Value = coin.Value.ToString();
                SubunitString = coin.IsSubunit == true ? Currency.SubunitName : "";
                if (coin.Coins != null)
                {
                    foreach (var coinQuality in coin.Coins)
                    {
                        Coins.Add(new QualityKeyValuePair<MoneyQuality, int>(coinQuality.Key, coinQuality.Value));
                    }
                }
                Description = coin.Description;
                ReversePicture = coin.ReversePicture;
                ObversePicture = coin.ObversePicture;
            }
            else
            {
                Era = Era.CE;
            }

        }

        private Dictionary<MoneyQuality, int> GetCoinsDictionary()
        {
            var coins = new Dictionary<MoneyQuality, int>();
            foreach (var coinPair in Coins)
            {
                coins.Add(coinPair.Key, coinPair.Value);

            }
            return coins;
        }
        public CoinDTO ToCoinDTO()
        {
            var issueDate = new Date();
            issueDate.Era = Era;
            if (!string.IsNullOrEmpty(Year))
            {
                issueDate.Year = int.Parse(Year);
            }
            var isSubunit = SubunitString != "" ? true : false;
            var coinDictionary = GetCoinsDictionary();
            int value = int.TryParse(Value, out var parsed) ? parsed : 0;
            var country = Country != null? Country.ToCountryDTO(): null;
            var currency = Currency != null? Currency.ToCurrencyDTO(): null;
            return new CoinDTO(Id, country, currency, value, Description, 0, ObversePicture, ReversePicture, issueDate, isSubunit, coinDictionary);
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Coins")
                {
                    if (Coins.Count == 0)
                    {
                        return "You must add at least one coin with his quality";
                    }
                }
                else if (columnName == "Value")
                {
                    if (string.IsNullOrEmpty(Value))
                    {
                        return null;
                    }
                    int value = StringToInt(Value);
                    if (value == -1 || value <= 0)
                    {
                        return "Invalid value.";
                    }
                }
                else if (columnName == "Year")
                {
                    if (string.IsNullOrEmpty(Value)) 
                    {
                        return null;
                    }
                    int year = StringToInt(Year);
                    if (year == -1 || (year > DateTime.Now.Year && Era == Era.CE))
                    {
                        return "Invalid year.";
                    }
                }
                return null;
            }
        }

        private int StringToInt(string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch
            {
                return -1;
            }
        }

        private readonly string[] _validatedProperties = { "Coins" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

