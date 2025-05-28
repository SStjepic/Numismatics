using Numismatics.CORE.DTO;
using Numismatics.WPF.ViewModel.CountryViewModel;
using Numismatics.WPF.ViewModel.CurrencyViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModel.CoinViewModel
{
    public class CoinSearchDataViewModel
    {

        public int Value { get; set; }
        public CountryDataViewModel Country { get; set; }
        public CurrencyDataViewModel Currency { get; set; }
        public int Year { get; set; }

        public CoinSearchDataViewModel()
        {
            Value = 0;
            Country = new CountryDataViewModel();
            Currency = new CurrencyDataViewModel();
            Year = 0;
        }

        public CoinSearchDataViewModel(int value, CountryDataViewModel country, CurrencyDataViewModel currency, int year)
        {
            Value = value;
            Country = country;
            Currency = currency;
            Year = year;
        }

        public CoinSearchDataDTO ToCoinSearchDataDTO()
        {
            return new CoinSearchDataDTO(Value, Country.ToCountryDTO(), Currency.ToCurrencyDTO(), Year);
        }
    }
}

