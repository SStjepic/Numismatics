using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class NationalCurrencyDTO
    {
        public long Id {  get; set; }    
        public CurrencyDTO Currency { get; set; }
        public CountryDTO Country { get; set; }

        public NationalCurrencyDTO()
        {
        }

        public NationalCurrencyDTO(long id, CurrencyDTO currency, CountryDTO country)
        {
            Id = id;
            Currency = currency;
            Country = country;
        }

        public NationalCurrencyDTO( CurrencyDTO currency, CountryDTO country)
        {
            Currency = currency;
            Country = country;
        }

        public NationalCurrency ToNationalCurrency()
        {
            return new NationalCurrency(Id, Currency.Id, Country.Id);
        }
    }
}
