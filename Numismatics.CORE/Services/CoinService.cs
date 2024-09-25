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
    public class CoinService : IService<CoinDTO>
    {
        private CoinRepository _coinRepository;
        public CoinService() 
        {
            _coinRepository = new CoinRepository();
        }
        public CoinDTO? Create(CoinDTO entity)
        {
            entity.Id = HashCode.Combine(entity.Value, entity.IssueDate, entity.Currency.Id, entity.Country.Id);
            _coinRepository.Create(entity.ToCoin());
            return entity;
        }

        public CoinDTO? Delete(CoinDTO entity)
        {
            _coinRepository.Delete(entity.ToCoin());
            return entity;
        }

        public CoinDTO? Get(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<CoinDTO> GetAll()
        {
            CurrencyRepository _currencyRepository = new CurrencyRepository();
            CountryRepository _countryRepository = new CountryRepository();
            var coins = _coinRepository.GetAll();
            var countries = _countryRepository.GetAll();
            var currencies = _currencyRepository.GetAll();
            var coinDTOs = new List<CoinDTO>();
            foreach(var coin in coins)
            {
                var country = countries.Find(c => c.Id == coin.CountryId);
                var currency = currencies.Find(c => c.Id == coin.CurrencyId);
                coinDTOs.Add(new CoinDTO(coin, country, currency));
            }
            return coinDTOs;
        }

        public CoinDTO? Update(CoinDTO entity)
        {
            _coinRepository.Update(entity.ToCoin());
            return entity;
        }
    }
}
