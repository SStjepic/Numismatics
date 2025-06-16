using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Interface
{
    public interface INationalCurrencyService: IService<NationalCurrencyDTO>
    {
        public List<NationalCurrencyDTO> GetAll(long currencyId);
        public List<CountryDTO> GetCountries(long currencyId);
        public List<CurrencyDTO> GetCurrencies(long countryId);
        public List<NationalCurrencyDTO> UpdateAll(List<NationalCurrencyDTO> nationalCurrencies, CurrencyDTO currency);
    }
}
