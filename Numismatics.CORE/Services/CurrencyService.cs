using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Repositories;
using Numismatics.CORE.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services
{
    public class CurrencyService : IService<CurrencyDTO>
    {
        private CurrencyRepository _currencyRepository;
        public CurrencyService() 
        {
            _currencyRepository = new CurrencyRepository();
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

        public List<CurrencyDTO> GetByPage(int pageNumber, int pageSize, object param)
        {
            var currencies = _currencyRepository.GetAll().Skip(pageNumber * pageSize).Take(pageSize);

            var searchParams = param as CurrencySearchDataDTO;
            if (searchParams != null)
            {
                if (!string.IsNullOrEmpty(searchParams.Name))
                {
                    currencies = currencies
                        .Where(c => c.Name.ToLower().Contains(searchParams.Name.ToLower()))
                        .ToList();
                }

                if (!string.IsNullOrEmpty(searchParams.Code))
                {
                    currencies = currencies
                        .Where(c => c.Code.ToLower().Contains(searchParams.Code.ToLower()))
                        .ToList();
                }
            }
            var selectedCurrencies = currencies.Skip(pageNumber * pageSize).Take(pageSize);
            var currenciesDTO = new List<CurrencyDTO>();
            foreach (Currency currency in selectedCurrencies)
            {
                currenciesDTO.Add(new CurrencyDTO(currency));
            }
            return currenciesDTO;
        }

        public int GetTotalPageNumber(int pageSize)
        {
            var totalItems = _currencyRepository.GetAll().Count();
            return (int)Math.Ceiling((double)totalItems / pageSize);
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
