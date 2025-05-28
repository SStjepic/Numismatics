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
    public class CoinService : IService<CoinDTO>
    {
        private CoinRepository _coinRepository;
        private ImageRepository _imageRepository;
        public CoinService() 
        {
            _coinRepository = new CoinRepository();
            _imageRepository = new ImageRepository();
        }
        public CoinDTO? Create(CoinDTO entity)
        {
            entity.Id = Math.Abs(HashCode.Combine(entity.Value,entity.HundertPart, entity.IssueDate, entity.Currency.Id, entity.Country.Id));
            (entity.ObversePicture, entity.ReversePicture) = _imageRepository.SaveCoinImage(entity.Id, entity.ObversePicture, entity.ReversePicture); ;
            _coinRepository.Create(entity.ToCoin());
            return entity;
        }

        public CoinDTO? Delete(CoinDTO entity)
        {
            _coinRepository.Delete(entity.Id);
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

        public List<CoinDTO> GetByPage(int pageNumber, int pageSize, object param)
        {
            CurrencyRepository _currencyRepository = new CurrencyRepository();
            CountryRepository _countryRepository = new CountryRepository();
            var countries = _countryRepository.GetAll();
            var currencies = _currencyRepository.GetAll();

            var coins = _coinRepository.GetAll();

            var searchParams = param as CoinSearchDataDTO;
            if (searchParams != null)
            {
                if (searchParams.Value > 0)
                {
                    coins = coins
                        .Where(b => b.Value == searchParams.Value)
                        .ToList();
                }

                if (searchParams.Year > 0)
                {
                    coins = coins
                        .Where(b => b.IssueDate.Year == searchParams.Year)
                        .ToList();
                }

                if (searchParams.Country.Id != -1)
                {
                    coins = coins
                        .Where(b => b.CountryId == searchParams.Country.Id)
                        .ToList();
                }

                if (searchParams.Currency.Id != -1)
                {
                    coins = coins
                        .Where(b => b.CurrencyId == searchParams.Currency.Id)
                        .ToList();
                }
            }
            var selectedCoins = coins.Skip((pageNumber) * pageSize).Take(pageSize).ToList();
            var coinDTOs = new List<CoinDTO>();
            foreach(var coin in coins)
            {
                var country = countries.Find(c => c.Id == coin.CountryId);
                var currency = currencies.Find(c => c.Id == coin.CurrencyId);
                coinDTOs.Add(new CoinDTO(coin, country, currency));
            }
            return coinDTOs;
        }

        public int GetTotalPageNumber(int pageSize)
        {
            var totalItems = _coinRepository.GetAll().Count();
            return (int)Math.Ceiling((double)totalItems / pageSize);
        }

        public CoinDTO? Update(CoinDTO entity)
        {
            (entity.ObversePicture, entity.ReversePicture) = _imageRepository.SaveCoinImage(entity.Id, entity.ObversePicture, entity.ReversePicture); ;
            _coinRepository.Update(entity.ToCoin());
            return entity;
        }
    }
}
