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

namespace Numismatics.WPF.ViewModels.BanknoteViewModel
{
    public class BanknoteDataViewModel: INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }

        private CountryDTO _country;
        public CountryDTO Country 
        { 
            get { return _country; }
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
        private string _day;
        public string Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }
        private string _month;
        public string Month
        {
            get { return _month; }
            set
            {
                _month = value;
                OnPropertyChanged(nameof(Month));
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

        private string _obversePicture;
        public string ObversePicture 
        { 
            get { return _obversePicture; }
            set
            {
                _obversePicture = value;
                OnPropertyChanged(nameof(ObversePicture));
            }
        }
        private string _reversePicture;
        public string ReversePicture 
        {
            get { return _reversePicture; }
            set
            {
                _reversePicture = value;
                OnPropertyChanged(nameof(ReversePicture));
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

        public ObservableCollection<QualityKeyValuePair<string, MoneyQuality>> Banknotes { get; set; }
        public BanknoteDataViewModel() { }
        public BanknoteDataViewModel(BanknoteDTO? banknoteDTO) 
        {
            Banknotes = new ObservableCollection<QualityKeyValuePair<string, MoneyQuality>>();
            if (banknoteDTO != null)
            {
                Id = banknoteDTO.Id;
                Country = banknoteDTO.Country;
                Currency = banknoteDTO.Currency;
                Value = banknoteDTO.Value.ToString();
                Day = banknoteDTO.IssueDate.Day != 0 ? banknoteDTO.IssueDate.Day.ToString() : "";
                Month = banknoteDTO.IssueDate.Month != 0 ? banknoteDTO.IssueDate.Month.ToString() : "";
                Year = banknoteDTO.IssueDate.Year != 0 ? banknoteDTO.IssueDate.Year.ToString() : "";
                ObversePicture = banknoteDTO.ObversePicture;
                ReversePicture = banknoteDTO.ReversePicture;
                Description = banknoteDTO.Description;
                City = banknoteDTO.City;
                if (banknoteDTO.Banknotes != null)
                {
                    foreach (var banknoteQuality in banknoteDTO.Banknotes)
                    {
                        Banknotes.Add(new QualityKeyValuePair<string, MoneyQuality>(banknoteQuality.Key, banknoteQuality.Value));
                    }
                }
            }
            else
            {
                
            }
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
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Country", "Currency", "Value" };

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

        public BanknoteDTO ToBanknoteDTO()
        {
            var banknotes = GetBanknotesDictionary();
            var issueDate = new Date(int.Parse(Day), int.Parse(Month), int.Parse(Year), Era);
            var value = int.Parse(Value);
            return new BanknoteDTO(Id, Country, Currency, value, ObversePicture, ReversePicture, Description, issueDate, City, banknotes);
        }

        private Dictionary<string, MoneyQuality> GetBanknotesDictionary()
        {
            var banknotesDictionary = new Dictionary<string, MoneyQuality>();
            foreach(var banknote in Banknotes)
            {
                banknotesDictionary.Add(banknote.Key, banknote.Value);
            }

            return banknotesDictionary;
        }
    }
}
