using Numismatics.CORE.DTO;
using Numismatics.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services
{
    public class NationalCurrencyService
    {
        private NationalCurrencyRepository _repository;
        private CurrencyRepository _currencyRepository;
        private CountryRepository _countryRepository;
        public NationalCurrencyService() 
        {
            _repository = new NationalCurrencyRepository();
            _currencyRepository = new CurrencyRepository();
            _countryRepository = new CountryRepository();
        }

        public NationalCurrencyDTO? Create(NationalCurrencyDTO nationalCurrencyDTO)
        {
            nationalCurrencyDTO.Id = DateTime.UtcNow.Ticks;
            _repository.Create(nationalCurrencyDTO.ToNationalCurrency());
            return nationalCurrencyDTO;
        }

        public List<CountryDTO> GetCountries(long id)
        {
            var countries = new List<CountryDTO>();

            foreach (var nationalCurrency in _repository.GetAll())
            {
                if (nationalCurrency.CurrencyId == id)
                {
                    foreach (var countryId in nationalCurrency.Countries)
                    {
                        var country = _countryRepository.Get(countryId);
                        countries.Add(new CountryDTO(country));
                    }
                }
            }

            return countries;
        }

        public List<CurrencyDTO> GetCurrencies(long countryId)
        {
            var currencies = new List<CurrencyDTO>();
            foreach(var nationalCurrency in _repository.GetAll())
            {
                if (nationalCurrency.Countries.Contains(countryId))
                {
                    var currency = _currencyRepository.Get(nationalCurrency.CurrencyId);
                    currencies.Add(new CurrencyDTO(currency));
                }
            }

            return currencies;
        }
    }
}
