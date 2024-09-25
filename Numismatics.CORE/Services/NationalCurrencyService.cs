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
        public NationalCurrencyService() 
        {
            _repository = new NationalCurrencyRepository();
            _currencyRepository = new CurrencyRepository();
        }

        public NationalCurrencyDTO? Create(NationalCurrencyDTO? nationalCurrencyDTO)
        {
            nationalCurrencyDTO.Id = HashCode.Combine(nationalCurrencyDTO.Currency.ToCurrency());
            _repository.Create(nationalCurrencyDTO.ToNationalCurrency());
            return nationalCurrencyDTO;
        }

        public List<int> GetCountriesByCurrency(int id)
        {
            var countryIds = new List<int>();

            foreach (var nationalCurrency in _repository.GetAll())
            {
                if (nationalCurrency.CurrencyId == id)
                {
                    foreach (var country in nationalCurrency.Countries)
                    {
                        countryIds.Add(country);
                    }
                }
            }

            return countryIds;
        }

        public List<CurrencyDTO> GetCurrencies(int countryId)
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
