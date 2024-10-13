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
            entity.Id = HashCode.Combine(entity.Name, entity.Code);
            _currencyRepository.Create(entity.ToCurrency());
            return entity;
        }

        public CurrencyDTO? Delete(CurrencyDTO entity)
        {
            _currencyRepository.Delete(entity.Id);
            return entity;
        }

        public CurrencyDTO? Get(int entityId)
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

        public List<CurrencyDTO> GetByPage(int pageNumber, int pageSize)
        {
            var currencies = _currencyRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var currenciesDTO = new List<CurrencyDTO>();
            foreach (Currency currency in currencies)
            {
                currenciesDTO.Add(new CurrencyDTO(currency));
            }
            return currenciesDTO;
        }

        public CurrencyDTO? Update(CurrencyDTO entity)
        {
            _currencyRepository.Update(entity.ToCurrency());
            return entity;
        }
    }
}
