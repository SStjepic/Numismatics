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
    public class CurrencyService : ICurrencyService
    {
        private ICurrencyRepository _currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public CurrencyDTO? Create(CurrencyDTO entity)
        {
            entity.Id = DateTime.UtcNow.Ticks;
            _currencyRepository.Create(entity.ToCurrency());
            return entity;
        }

        public CurrencyDTO? Delete(CurrencyDTO entity)
        {
            _currencyRepository.Delete(entity.Id);
            return entity;
        }

        public CurrencyDTO? Get(long entityId)
        {
            throw new NotImplementedException();
        }

        public List<CurrencyDTO> GetAll()
        {
            var currencies = _currencyRepository.GetAll();
            var currenciesDTO = new List<CurrencyDTO>();
            foreach(Currency currency in currencies )
            {
                currenciesDTO.Add(new CurrencyDTO(currency));
            }
            return currenciesDTO;
        }

        public List<CurrencyDTO> GetByPage(int pageNumber, int pageSize, CurrencySearchDataDTO searchParams)
        {
            
            var currenciesDTO = new List<CurrencyDTO>();
            var selectedCurrencies = _currencyRepository.GetByPage(pageNumber, pageSize, searchParams);
            foreach (Currency currency in selectedCurrencies)
            {
                currenciesDTO.Add(new CurrencyDTO(currency));
            }
            return currenciesDTO;
        }

        public int GetTotalPageNumber(int pageSize)
        {
            var totalItems = _currencyRepository.GetAll().Count();
            return Math.Max(1, (int)Math.Ceiling((double)totalItems / pageSize));
        }


        public CurrencyDTO? Update(CurrencyDTO entity)
        {
            _currencyRepository.Update(entity.ToCurrency());
            return entity;
        }

        public int GetTotalCurrenciesNumber()
        {
            return _currencyRepository.GetTotalCurrenciesNumber();
        }
    }
}
