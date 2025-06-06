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
        public List<CountryDTO> GetCountries(long currencyId);
        public List<CurrencyDTO> GetCurrencies(long countryId);
    }
}
