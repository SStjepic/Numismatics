using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.NationalCurrencyViewModel
{
    public class NationalCurrencyViewModel
    {
        private NationalCurrencyService _nationalCurrencyService;
        private CurrencyService _currencyService;
        private CountryService _countryService;

        public NationalCurrencyViewModel()
        {
            _countryService = new CountryService();
            _currencyService = new CurrencyService();
            _nationalCurrencyService = new NationalCurrencyService();
        }

        public List<CountryDTO> GetCountriesByCurrency(CurrencyDTO currencyDTO)
        {
            var countryIds = _nationalCurrencyService.GetCountriesByCurrency(currencyDTO.Id);
            var countries = new List<CountryDTO>();
            foreach (var countryId in countryIds)
            {
                var countryDTO = _countryService.Get(countryId);
                countries.Add(countryDTO);
            }
            return countries;
        }

        public NationalCurrencyDTO CreateNationalCurrency(CurrencyDTO currencyDTO, List<CountryDTO> countryDTOs)
        {
            var newNationalCurrency = new NationalCurrencyDTO(currencyDTO, countryDTOs);
            newNationalCurrency = _nationalCurrencyService.Create(newNationalCurrency);
            return newNationalCurrency;

        }
    }
}
