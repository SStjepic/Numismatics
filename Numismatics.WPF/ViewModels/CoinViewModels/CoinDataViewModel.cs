using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
using Numismatics.WPF.Utils;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.WPF.ViewModels.CoinViewModels
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
        private string _unitName;
        public string UnitName
        {
            get { return _unitName; }
            set
            {
                _unitName = value;
                OnPropertyChanged(nameof(UnitName));
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

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private ObservableCollection<OwnedCoinDataViewModel> _coins;
        public ObservableCollection<OwnedCoinDataViewModel> Coins
        {
            get { return _coins; }
            set
            {
                _coins = value;
                OnPropertyChanged(nameof(Coins));
            }
        }
        public string CurrentCoinQuality { get; set; }
        private OwnedCoinDataViewModel _currentOwnedCoin;
        public OwnedCoinDataViewModel CurrentOwnedCoin
        {
            get { return _currentOwnedCoin; }
            set
            {
                _currentOwnedCoin = value;
                OnPropertyChanged(nameof(CurrentOwnedCoin));
            }
        }

        public CoinDataViewModel() { }
        public CoinDataViewModel(CoinDTO coin)
        {
            Coins = new ObservableCollection<OwnedCoinDataViewModel>();
            if (coin != null)
            {
                Id = coin.Id;
                Country = new CountryDataViewModel(coin.Country);
                Currency = new CurrencyDataViewModel(coin.Currency);
                Year = coin.IssueDate.Year != 0? coin.IssueDate.Year.ToString(): "";
                Era = coin.IssueDate.Era;
                Value = coin.Value != 0? coin.Value.ToString(): "";
                UnitName = coin.IsSubunit == true ? Currency.SubunitName : Currency.MainUnitName;
                if (coin.OwnedCoins != null)
                {
                    foreach (var ownedCoin in coin.OwnedCoins)
                    {
                        Coins.Add(new OwnedCoinDataViewModel(ownedCoin));
                    }
                }
                Description = coin.Description;
                ReversePicture = coin.ReversePicture;
                ObversePicture = coin.ObversePicture;
                City = coin.City;
            }
            else
            {
                Era = Era.CE;
            }

        }
        public CoinDTO ToCoinDTO()
        {
            var issueDate = new Date();
            issueDate.Era = Era;
            if (!string.IsNullOrEmpty(Year))
            {
                issueDate.Year = int.Parse(Year);
            }
            var isSubunit = false;
            if (UnitName != null && Currency != null)
            {
                isSubunit = string.Equals(UnitName, Currency.SubunitName) ? true : false;
            }
            int value = int.TryParse(Value, out var parsed) ? parsed : 0;
            var country = Country != null? Country.ToCountryDTO(): null;
            var currency = Currency != null? Currency.ToCurrencyDTO(): null;
            var ownedCoins = ToOwnedCoinDTOs();
            return new CoinDTO(Id, country, currency, value, issueDate, Description, ObversePicture, ReversePicture, isSubunit, ownedCoins,City);
        }

        private List<OwnedCoinDTO> ToOwnedCoinDTOs()
        {
            var ownedCoins = new List<OwnedCoinDTO>();
            foreach (var coin in Coins)
            {
                ownedCoins.Add(coin.ToOwnedCoinDTO());
            }

            return ownedCoins;
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
                    if (string.IsNullOrEmpty(Year)) 
                    {
                        return null;
                    }
                    int year = StringToInt(Year);
                    if (year == -1 || year > DateTime.Now.Year && Era == Era.CE)
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

