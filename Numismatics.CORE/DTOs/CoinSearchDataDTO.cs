using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class CoinSearchDataDTO
    {
        public int Value { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }
        public int Year { get; set; }

        public CoinSearchDataDTO() { }
        public CoinSearchDataDTO(int value, CountryDTO country, CurrencyDTO currency, int year)
        {
            Value = value;
            Country = country;
            Currency = currency;
            Year = year;
        }
    }
}
