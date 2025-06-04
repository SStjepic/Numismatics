using Numismatics.CORE.DTOs;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.BanknoteViewModels
{
    public class BanknoteSearchDataViewModel
    {
        public int Value { get; set; }
        public CountryDataViewModel Country { get; set; }
        public CurrencyDataViewModel Currency { get; set; }
        public int Year { get; set; }

        public BanknoteSearchDataViewModel() 
        {
            Value = 0;
            Country = new CountryDataViewModel();
            Currency = new CurrencyDataViewModel();
            Year = 0;
        }

        public BanknoteSearchDataViewModel(int value, CountryDataViewModel country, CurrencyDataViewModel currency, int year)
        {
            Value = value;
            Country = country;
            Currency = currency;
            Year = year;
        }

        public BanknoteSearchDataDTO ToBanknoteSearchDataDTO()
        {
            return new BanknoteSearchDataDTO(Value, Country.ToCountryDTO(), Currency.ToCurrencyDTO(), Year);
        }
    }
}
