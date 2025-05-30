using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTO
{
    public class NationalCurrencyDTO
    {
        public long Id {  get; set; }    
        public CurrencyDTO Currency { get; set; }
        public List<CountryDTO> CountryDTOs { get; set; }

        public NationalCurrencyDTO()
        {
        }

        public NationalCurrencyDTO(long id, CurrencyDTO currency, List<CountryDTO> countryDTOs)
        {
            Id = id;
            Currency = currency;
            CountryDTOs = countryDTOs;
        }

        public NationalCurrencyDTO( CurrencyDTO currency, List<CountryDTO> countryDTOs)
        {
            Currency = currency;
            CountryDTOs = countryDTOs;
        }

        public void AddCountry(CountryDTO countryDTO)
        {
            CountryDTOs.Add(countryDTO);
        }

        public NationalCurrency ToNationalCurrency()
        {
            var countries = new List<long>();
            foreach (var country in CountryDTOs)
            { 
                countries.Add(country.Id);
            }
            return new NationalCurrency(Id, Currency.Id, countries);
        }
    }
}
