using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services.CountryService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.CountryViewModel
{
    public class CountryDataViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        public int Id { get; set; } 
        private string _name;
        private string _capital;
        private string _bank;
        private int _startYear;
        private Era _startYearEra;
        private int _endYear;
        private Era _endYearEra;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Capital
        {
            get { return _capital; }
            set
            {
                _capital = value;
                OnPropertyChanged(nameof(Capital));
            }
        }

        public string Bank
        {
            get { return _bank; }
            set
            {
                _bank = value;
                OnPropertyChanged(nameof(Bank));
            }
        }

        public int StartYear
        {
            get { return _startYear; }
            set
            {
                _startYear = value;
                OnPropertyChanged(nameof(StartYear));
            }
        }

        public Era SrartYearEra
        {
            get { return _startYearEra; }
            set
            {
                _startYearEra = value;
                OnPropertyChanged(nameof(SrartYearEra));
            }
        }

        public int EndYear
        {
            get { return _endYear; }
            set
            {
                _endYear = value;
                OnPropertyChanged(nameof(EndYear));
            }
        }

        public Era EndYearEra
        {
            get { return _endYearEra; }
            set
            {
                _endYearEra = value;
                OnPropertyChanged(nameof(EndYearEra));
            }
        }

        public CountryDataViewModel(CountryDTO? country)
        {
            if(country != null)
            {
                Id = country.Id;
                Name = country.Name;
                Capital = country.Capital;
                Bank = country.Bank;
                StartYear = country.StartYear.Year;
                EndYear = country.EndYear.Year; 
            }
        }
        public CountryDTO ToCountryDTO()
        {
            return new CountryDTO(Id, Name, Capital, Bank, new Date(StartYear, SrartYearEra), new Date(EndYear, EndYearEra));
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

        private readonly string[] _validatedProperties = { "Name", "Capital", "Bank", "StartYear", "EndYear", "StartYearEra", "EndYearEra" };

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
