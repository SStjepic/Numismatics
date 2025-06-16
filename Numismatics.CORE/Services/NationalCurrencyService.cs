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
                    var country = _countryRepository.Get(nationalCurrency.CountryId);
                    countries.Add(new CountryDTO(country));
                }
            }

            return countries;
        }

        public List<CurrencyDTO> GetCurrencies(long countryId)
        {
            var currencies = new List<CurrencyDTO>();
            foreach(var nationalCurrency in _nationalCurrencyRepository.GetAll())
            {
                if (nationalCurrency.CountryId == countryId)
                {
                    var currency = _currencyRepository.Get(nationalCurrency.CurrencyId);
                    currencies.Add(new CurrencyDTO(currency));
                }
            }

            return currencies;
        }

        public List<NationalCurrencyDTO>? GetAll(long currencyId)
        {
            var nationalCurrencyDTOs = new List<NationalCurrencyDTO>();
            var nationalCurrencies = _nationalCurrencyRepository.GetByCurrency(currencyId);
            var currency = _currencyRepository.Get(currencyId);
            foreach (var nationalCurrency in nationalCurrencies)
            {
                var country = _countryRepository.Get(nationalCurrency.CountryId);
                nationalCurrencyDTOs.Add(new NationalCurrencyDTO(nationalCurrency.Id, new CurrencyDTO(currency), new CountryDTO(country)));
            }
            return nationalCurrencyDTOs;
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

        public NationalCurrencyDTO? Get(long entityId)
        {
            throw new NotImplementedException();
        }

        public List<NationalCurrencyDTO> UpdateAll(List<NationalCurrencyDTO> nationalCurrencies, CurrencyDTO currency)
        {
            var oldNationalCurrencies = _nationalCurrencyRepository.GetByCurrency(currency.Id);
            foreach (var nationalCurrency in nationalCurrencies)
            {
                if (nationalCurrency.Id == -1)
                {
                    this.Create(nationalCurrency);
                }
                else
                {
                    this.Update(nationalCurrency);
                }
            }

            var old = oldNationalCurrencies.Except(nationalCurrencies.Select(x => x.ToNationalCurrency()).ToList()).ToList();
            foreach(var nationalCurrency in old)
            {
                _nationalCurrencyRepository.Delete(nationalCurrency.Id);
            }
            return nationalCurrencies;
        }
    }
}
