using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Numismatics.WPF.ViewModels.CoinViewModel
{
    public class QualityKeyValuePair
    {
        public BanknoteQuality Key { get; set; }
        public int Value { get; set; }

        public QualityKeyValuePair() { }
        public QualityKeyValuePair(BanknoteQuality key, int value)
        {
            Key = key;
            Value = value;
        }
    }
    public class CoinDataViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        public CoinDataViewModel() { }
        public CoinDataViewModel(CoinDTO? coin) 
        {
            Coins = new ObservableCollection<QualityKeyValuePair>();
            if (coin != null) 
            {
                Id = coin.Id;
                Country = coin.Country;
                Currency = coin.Currency;
                Year = coin.IssueDate.Year;
                Era = coin.IssueDate.Era;
                Value = coin.Value;
                HundertPart = coin.HundertPart == true ? Currency.HunderthPartName : "";
                if(coin.Coins != null)
                {
                    foreach (var coinQuality in coin.Coins)
                    {
                        Coins.Add(new QualityKeyValuePair(coinQuality.Key, coinQuality.Value));
                    }
                }
                Description = coin.Description;
                ReversePicture = coin.ReversePicture;
                ObversePicture = coin.ObversePicture;
            }
            
        }

        private Dictionary<BanknoteQuality, int> GetCoinsDictionary()
        {
            var coins = new Dictionary<BanknoteQuality, int>();
            foreach(var coinPair in Coins)
            {
                coins.Add(coinPair.Key, coinPair.Value);

            }
            return coins;
        }
        public CoinDTO ToCoinDTO()
        {
            var issueDate = new Date(Year, Era);
            var hundertPart = HundertPart != "" ? true : false;
            var coinDictionary = GetCoinsDictionary();
            return new CoinDTO(Id, Country, Currency, Value, Description, 0, ObversePicture, ReversePicture, issueDate, hundertPart, coinDictionary);
        }

        public int Id { get; set; }
        private CountryDTO _country;
        public CountryDTO Country
        {
            get {  return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        private CurrencyDTO _currency;
        public CurrencyDTO Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }
        private double _value;
        public double Value 
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        private string _hundertPart;
        public string HundertPart
        {
            get { return _hundertPart; }
            set
            {
                _hundertPart = value;
                OnPropertyChanged(nameof(HundertPart));
            }
        }

        private int _year;
        public int Year
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

        public ObservableCollection<QualityKeyValuePair> Coins
        {
            get; set;   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if(columnName == "Coins")
                {
                    if(Coins.Count == 0)
                    {
                        return "You must add at least one coin with his quality";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Country", "Currency", "Value","Coins" };

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
    }
}
