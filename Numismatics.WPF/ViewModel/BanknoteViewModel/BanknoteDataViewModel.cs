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
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.WPF.ViewModel.BanknoteViewModel
{
    public class BanknoteDataViewModel: INotifyPropertyChanged, IDataErrorInfo
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

        public Date IssueDate { get; set; }

        public string CurrentBanknoteQuality { get; set; }
        public string CurrentBanknoteCode {  get; set; }

        private QualityKeyValuePair<string, MoneyQuality> _currentBanknotePair;
        public QualityKeyValuePair<string, MoneyQuality> CurentBanknotePair
        {
            get { return _currentBanknotePair; }
            set
            {
                _currentBanknotePair = value;
                OnPropertyChanged(nameof(CurentBanknotePair));
            }
        }
        public ObservableCollection<QualityKeyValuePair<string, MoneyQuality>> Banknotes { get; set; }
        public BanknoteDataViewModel() { }
        public BanknoteDataViewModel(BanknoteDTO banknoteDTO) 
        {
            Banknotes = new ObservableCollection<QualityKeyValuePair<string, MoneyQuality>>();
            if (banknoteDTO != null)
            {
                Id = banknoteDTO.Id;
                Country = new CountryDataViewModel(banknoteDTO.Country);
                Currency = new CurrencyDataViewModel(banknoteDTO.Currency);
                Value = banknoteDTO.Value.ToString();
                HundertPart = banknoteDTO.HundertPart == true ? Currency.HunderthPartName : "";
                Day = banknoteDTO.IssueDate.Day != 0 ? banknoteDTO.IssueDate.Day.ToString() : "";
                Month = banknoteDTO.IssueDate.Month != 0 ? banknoteDTO.IssueDate.Month.ToString() : "";
                Year = banknoteDTO.IssueDate.Year != 0 ? banknoteDTO.IssueDate.Year.ToString() : "";
                IssueDate = banknoteDTO.IssueDate;
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
        }

        private bool IsBanknoteCodeExist()
        {
            var item = Banknotes.FirstOrDefault(b => b.Key == CurrentBanknoteCode);
            if(item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddBanknoteQuality()
        {
            if(CurrentBanknoteQuality == null)
            {
                MessageBox.Show("Please select a banknote quality", "Error");
                return;
            }
            if (string.IsNullOrEmpty(CurrentBanknoteCode))
            {
                MessageBox.Show("Enter banknote code", "Error");
                return;
            }
            if (IsBanknoteCodeExist())
            {
                MessageBox.Show($"You already have banknote with this code {CurrentBanknoteCode}", "Notification");
                return;
            }
            MoneyQuality banknoteQuality = (MoneyQuality)Enum.Parse(typeof(MoneyQuality), CurrentBanknoteQuality);
            QualityKeyValuePair<string, MoneyQuality> banknote = new QualityKeyValuePair<string, MoneyQuality>();
            banknote.Key = CurrentBanknoteCode;
            banknote.Value = banknoteQuality;
            Banknotes.Add(banknote);

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
                if(columnName == "Banknotes")
                {
                    if (Banknotes.Count == 0)
                    {
                        return "You must add at least one banknote with his code and quality.";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Banknotes" };

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
            var issueDate = new Date();
            if(Day != "" && Day != null)
            {
                issueDate.Day = int.Parse(Day);
            }
            if (Month != "" && Month != null)
            {
                issueDate.Month = int.Parse(Month);
            }
            if (Year != "" && Year != null)
            {
                issueDate.Year = int.Parse(Year);
            }
            var value = int.Parse(Value);
            var hundertPart = HundertPart != "" ? true : false;
            return new BanknoteDTO(Id, Country.ToCountryDTO(), Currency.ToCurrencyDTO(), value, hundertPart, ObversePicture, ReversePicture, Description, issueDate, City, banknotes);
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
