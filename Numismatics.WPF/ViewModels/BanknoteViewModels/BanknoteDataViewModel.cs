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
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.WPF.ViewModels.BanknoteViewModels
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

        private OwnedBanknoteDataViewModel _currentOwnedBanknote;
        public OwnedBanknoteDataViewModel CurrentOwnedBanknote
        {
            get { return _currentOwnedBanknote; }
            set
            {
                _currentOwnedBanknote = value;
                OnPropertyChanged(nameof(CurrentOwnedBanknote));
            }
        }
        public ObservableCollection<OwnedBanknoteDataViewModel> Banknotes { get; set; }
        public BanknoteDataViewModel() { }
        public BanknoteDataViewModel(BanknoteDTO banknoteDTO) 
        {
            Banknotes = new ObservableCollection<OwnedBanknoteDataViewModel>();
            if (banknoteDTO != null)
            {
                Id = banknoteDTO.Id;
                Country = new CountryDataViewModel(banknoteDTO.Country);
                Currency = new CurrencyDataViewModel(banknoteDTO.Currency);
                Value = banknoteDTO.Value != 0?banknoteDTO.Value.ToString():"";
                UnitName = banknoteDTO.IsSubunit == true ? Currency.SubunitName : Currency.MainUnitName;
                Day = banknoteDTO.IssueDate.Day != 0 ? banknoteDTO.IssueDate.Day.ToString() : "";
                Month = banknoteDTO.IssueDate.Month != 0 ? banknoteDTO.IssueDate.Month.ToString() : "";
                Year = banknoteDTO.IssueDate.Year != 0 ? banknoteDTO.IssueDate.Year.ToString() : "";
                IssueDate = banknoteDTO.IssueDate;
                ObversePicture = banknoteDTO.ObversePicture;
                ReversePicture = banknoteDTO.ReversePicture;
                Description = banknoteDTO.Description;
                City = banknoteDTO.City;
                Era = banknoteDTO.IssueDate.Era;
                if (banknoteDTO.OwnedBanknotes != null)
                {
                    foreach (var banknote in banknoteDTO.OwnedBanknotes)
                    {
                        Banknotes.Add(new OwnedBanknoteDataViewModel(banknote));
                    }
                }
            }
            else
            {
                Era = Era.CE;
            }
        }

        private bool IsBanknoteCodeExist()
        {
            var item = Banknotes.FirstOrDefault(b => b.Code == CurrentBanknoteCode);
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
            OwnedBanknoteDataViewModel ownedBanknote = new OwnedBanknoteDataViewModel(CurrentBanknoteCode, banknoteQuality, Id);
            Banknotes.Add(ownedBanknote);

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
                else if (columnName == "Day")
                {
                    if (string.IsNullOrEmpty(Day))
                    {
                        return null;
                    }
                    int day = StringToInt(Day);
                    if(day == -1 || day <= 0 )
                    {
                        return "Invalid day.";
                    }
                }
                else if (columnName == "Month")
                {
                    if (string.IsNullOrEmpty(Month))
                    {
                        return null;
                    }
                    int month = StringToInt(Month);
                    if (month == -1 || month < 1 || month > 12)
                    {
                        return "Invalid month.";
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
                else if(columnName == "Value")
                {
                    if (string.IsNullOrEmpty(Value))
                    {
                        return null;
                    }
                    int value = StringToInt(Value);
                    if(value == -1 || value <= 0)
                    {
                        return "Invalid value.";
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

        private readonly string[] _validatedProperties = { "Banknotes", "Day", "Month", "Year", "Value"};

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
            var banknotes = ToBanknotesDTO();
            var issueDate = new Date();
            issueDate.Era = Era;
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
            int value = int.TryParse(Value, out var parsed) ? parsed : 0;
            var isSubunit = false;
            if (UnitName != null && Currency != null) 
            {
                isSubunit = string.Equals(UnitName, Currency.SubunitName) ? true : false;
            }
            var country = Country != null ? Country.ToCountryDTO() : null;
            var currency = Currency != null ? Currency.ToCurrencyDTO() : null;
            return new BanknoteDTO(Id, country, currency, value, isSubunit, ObversePicture, ReversePicture, Description, issueDate, City, banknotes);
        }

        private  List<OwnedBanknoteDTO> ToBanknotesDTO()
        {
            var ownedBanknotes = new List<OwnedBanknoteDTO>();
            foreach(var banknote in Banknotes)
            {
                ownedBanknotes.Add(banknote.ToOwnedBanknoteDTO());
            }

            return ownedBanknotes;
        }

        public override bool Equals(object obj)
        {
            return obj is BanknoteDataViewModel model &&
                   Id == model.Id;
        }
    }
}
