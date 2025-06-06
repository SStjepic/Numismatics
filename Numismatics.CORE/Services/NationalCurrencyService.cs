using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
using Numismatics.CORE.Repositories;
using Numismatics.CORE.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services
{
    public class NationalCurrencyService: INationalCurrencyService
    {
        private INationalCurrencyRepository _nationalCurrencyRepository;
        private ICurrencyRepository _currencyRepository;
        private ICountryRepository _countryRepository;
        public NationalCurrencyService(INationalCurrencyRepository nationalCurrencyRepository, ICurrencyRepository currencyRepository, ICountryRepository countryRepository)
        {
            _nationalCurrencyRepository = nationalCurrencyRepository;
            _currencyRepository = currencyRepository;
            _countryRepository = countryRepository;
        }

        public NationalCurrencyDTO? Create(NationalCurrencyDTO nationalCurrencyDTO)
        {
            nationalCurrencyDTO.Id = DateTime.UtcNow.Ticks;
            _nationalCurrencyRepository.Create(nationalCurrencyDTO.ToNationalCurrency());
            return nationalCurrencyDTO;
        }

        public NationalCurrencyDTO Update(NationalCurrencyDTO nationalCurrencyDTO)
        {
            _nationalCurrencyRepository.Update(nationalCurrencyDTO.ToNationalCurrency());
            return nationalCurrencyDTO;
        }
        public List<CountryDTO> GetCountries(long currencyId)
        {
            var countries = new List<CountryDTO>();

            foreach (var nationalCurrency in _nationalCurrencyRepository.GetAll())
            {
                if (nationalCurrency.CurrencyId == currencyId)
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
            foreach(var nationalCurrency in _nationalCurrencyRepository.GetAll())
            {
                if (nationalCurrency.Countries.Contains(countryId))
                {
                    var currency = _currencyRepository.Get(nationalCurrency.CurrencyId);
                    currencies.Add(new CurrencyDTO(currency));
                }
            }

            return currencies;
        }

        public NationalCurrencyDTO? Get(long currencyId)
        {
            var nationalCurrency = _nationalCurrencyRepository.Get(currencyId);
            if(nationalCurrency == null)
            {
                return null;
            }
            var countriesDTO = GetCountries(currencyId);
            var currency = _currencyRepository.Get(currencyId);
            return new NationalCurrencyDTO(nationalCurrency.Id, new CurrencyDTO(currency), countriesDTO);
        }

        public NationalCurrencyDTO? Delete(NationalCurrencyDTO entity)
        {
            throw new NotImplementedException();
        }

        public List<NationalCurrencyDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetTotalPageNumber(int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
