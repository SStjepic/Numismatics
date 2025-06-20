﻿using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.DTOs;
using Numismatics.WPF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.CountryViewModels
{
    public class CountryDataViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public long Id { get; set; }
        private string _name;
        private string _capital;
        private string _bank;
        private string _startYear;
        private Era _startYearEra;
        private string _endYear;
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

        public string StartYear
        {
            get { return _startYear; }
            set
            {
                _startYear = value;
                OnPropertyChanged(nameof(StartYear));
            }
        }

        public Era StartYearEra
        {
            get { return _startYearEra; }
            set
            {
                _startYearEra = value;
                OnPropertyChanged(nameof(StartYearEra));
            }
        }

        public string EndYear
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

        public CountryDataViewModel() { }
        public CountryDataViewModel(CountryDTO country)
        {
            if(country == null || country.Id == -1)
            {
                SetYears(-1, -1);
            }
            else
            {
                Id = country.Id;
                Name = country.Name;
                Capital = country.Capital;
                Bank = country.Bank;
                SetYears(country.StartYear.Year, country.EndYear.Year);
                StartYearEra = country.StartYear.Era;
                EndYearEra = country.EndYear.Era;

            }
        }
        public CountryDTO ToCountryDTO()
        {
            return new CountryDTO(Id, Name, Capital, Bank, new Date(StringToInt(StartYear), StartYearEra), new Date(StringToInt(EndYear), EndYearEra));
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
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Enter a country name.";
                    }
                }

                else if (columnName == "Capital")
                {
                    if (string.IsNullOrEmpty(Capital))
                    {
                        return "Enter a country capital.";
                    }

                }

                else if (columnName == "StartYear")
                {
                    var year = StringToInt(StartYear);
                    if (StartYear != GlobalParams.EMPTY_VALUE && year == -1)
                    {
                        return "Enter a correct year.";
                    }
                    else if (year <= 0 && year != -1)
                    {
                        return "Enter a valid year.";
                    }
                    else if (StartYearEra == Era.CE && year > DateTime.Now.Year)
                    {
                        return "Enter a correct year.";
                    }

                }

                else if (columnName == "EndYear")
                {
                    var year = StringToInt(EndYear);
                    if (EndYear != GlobalParams.EMPTY_VALUE && year == -1)
                    {
                        return "Enter a correct year";
                    }
                    else if (year <= 0 && year != -1)
                    {
                        return "Enter a valid year.";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "Capital", "StartYear", "EndYear" };

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

        private void SetYears(int startYear, int endYear)
        {
            StartYear = startYear == -1 ? GlobalParams.EMPTY_VALUE : startYear.ToString();
            EndYear = endYear == -1 ? GlobalParams.EMPTY_VALUE : endYear.ToString();

        }

        public override bool Equals(object obj)
        {
            return obj is CountryDataViewModel model &&
                   Id == model.Id;
        }
    }
}
