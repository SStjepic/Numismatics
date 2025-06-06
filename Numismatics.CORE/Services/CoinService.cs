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
    public class CoinService : ICoinService
    {
        private ICoinRepository _coinRepository;
        private IImageRepository _imageRepository;
        private ICurrencyRepository _currencyRepository;
        private ICountryRepository _countryRepository;

        public CoinService(ICoinRepository coinRepository, IImageRepository imageRepository, ICurrencyRepository currencyRepository, ICountryRepository countryRepository)
        {
            _coinRepository = coinRepository;
            _imageRepository = imageRepository;
            _currencyRepository = currencyRepository;
            _countryRepository = countryRepository;
        }

        public CoinDTO? Create(CoinDTO newCoin)
        {
            newCoin.Id = DateTime.UtcNow.Ticks;
            (newCoin.ObversePicture, newCoin.ReversePicture) = _imageRepository.SaveCoinImage(newCoin.Id, newCoin.ObversePicture, newCoin.ReversePicture); ;
            _coinRepository.Create(newCoin.ToCoin());
            return newCoin;
        }

        public CoinDTO? Delete(CoinDTO coin)
        {
            _imageRepository.DeleteCoin(coin.Id);
            _coinRepository.Delete(coin.Id);
            return coin;
        }

        public CoinDTO? Get(long entityId)
        {
            throw new NotImplementedException();
        }

        public List<CoinDTO> GetAll()
        {
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

        public List<CoinDTO> GetByPage(int pageNumber, int pageSize, CoinSearchDataDTO searchParams)
        {
            var countries = _countryRepository.GetAll();
            var currencies = _currencyRepository.GetAll();

            var coins = _coinRepository.GetAll();

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

                if (searchParams.Country.Id > 0)
                {
                    coins = coins
                        .Where(b => b.CountryId == searchParams.Country.Id)
                        .ToList();
                }

                if (searchParams.Currency.Id > 0)
                {
                    coins = coins
                        .Where(b => b.CurrencyId == searchParams.Currency.Id)
                        .ToList();
                }
            }

            coins = coins
               .OrderByDescending(b => b.IssueDate)
               .ThenBy(b => b.IsSubunit)
               .ThenByDescending(b => b.Value)
               .ToList();
            var selectedCoins = coins.Skip(pageNumber * pageSize).Take(pageSize).ToList();
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
            return Math.Max(1, (int)Math.Ceiling((double)totalItems / pageSize));
        }

        public CoinDTO? Update(CoinDTO entity)
        {
            (entity.ObversePicture, entity.ReversePicture) = _imageRepository.SaveCoinImage(entity.Id, entity.ObversePicture, entity.ReversePicture); ;
            _coinRepository.Update(entity.ToCoin());
            return entity;
        }

        public int GetTotalCoinsNumber()
        {
            return _coinRepository.GetTotalCoinsNumber();
        }
    }
}
