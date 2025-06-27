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
    public class CoinService : ICoinService
    {
        private ICoinRepository _coinRepository;
        private IImageRepository _imageRepository;
        private ICurrencyRepository _currencyRepository;
        private ICountryRepository _countryRepository;
        private IOwnedCoinRepository _ownedCoinRepository;

        public CoinService(ICoinRepository coinRepository, IImageRepository imageRepository, ICurrencyRepository currencyRepository, ICountryRepository countryRepository, IOwnedCoinRepository ownedCoinRepository)
        {
            _coinRepository = coinRepository;
            _imageRepository = imageRepository;
            _currencyRepository = currencyRepository;
            _countryRepository = countryRepository;
            _ownedCoinRepository = ownedCoinRepository;
        }

        public CoinDTO? Create(CoinDTO newCoin)
        {
            newCoin.Id = DateTime.UtcNow.Ticks;
            (newCoin.ObversePicture, newCoin.ReversePicture) = _imageRepository.SaveCoinImage(newCoin.Id, newCoin.ObversePicture, newCoin.ReversePicture); ;
            _coinRepository.Create(newCoin.ToCoin());
            foreach (OwnedCoinDTO ownedCoin in newCoin.OwnedCoins)
            {
                ownedCoin.Id = DateTime.UtcNow.Ticks;
                ownedCoin.CoinId = newCoin.Id;
                _ownedCoinRepository.Create(ownedCoin.ToOwnedCoin());
            }
            return newCoin;
        }

        public CoinDTO? Delete(CoinDTO coin)
        {
            _imageRepository.DeleteCoin(coin.Id);
            _coinRepository.Delete(coin.Id);
            foreach (OwnedCoinDTO ownedCoin in coin.OwnedCoins)
            {
                _ownedCoinRepository.Delete(ownedCoin.Id);
            }
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
                var ownedCoins = _ownedCoinRepository.GetByCoin(coin.Id);
                var country = countries.Find(c => c.Id == coin.CountryId);
                var currency = currencies.Find(c => c.Id == coin.CurrencyId);
                coinDTOs.Add(new CoinDTO(coin, country, currency, ToOwnedCoins(ownedCoins)));
            }
            return coinDTOs;
        }

        private List<OwnedCoinDTO> ToOwnedCoins(List<OwnedCoin> ownedCoins)
        {
            var DTOs = new List<OwnedCoinDTO>();
            foreach (var coin in ownedCoins)
            {
                DTOs.Add(new OwnedCoinDTO(coin));
            }
            return DTOs;
        }

        public List<CoinDTO> GetByPage(int pageNumber, int pageSize, CoinSearchDataDTO searchParams)
        {
            var countries = _countryRepository.GetAll();
            var currencies = _currencyRepository.GetAll();
            var selectedCoins = _coinRepository.GetByPage(pageNumber, pageSize, searchParams);
           
            var coinDTOs = new List<CoinDTO>();
            foreach(var coin in selectedCoins)
            {
                var ownedCoins = _ownedCoinRepository.GetByCoin(coin.Id);
                var country = countries.Find(c => c.Id == coin.CountryId);
                var currency = currencies.Find(c => c.Id == coin.CurrencyId);
                coinDTOs.Add(new CoinDTO(coin, country, currency, ToOwnedCoins(ownedCoins)));
            }
            return coinDTOs;
        }

        public int GetTotalPageNumber(int pageSize)
        {
            var totalItems = _coinRepository.GetAll().Count();
            return Math.Max(1, (int)Math.Ceiling((double)totalItems / pageSize));
        }

        public CoinDTO? Update(CoinDTO coinDTO)
        {
            (coinDTO.ObversePicture, coinDTO.ReversePicture) = _imageRepository.SaveCoinImage(coinDTO.Id, coinDTO.ObversePicture, coinDTO.ReversePicture); ;
            _coinRepository.Update(coinDTO.ToCoin());
            var ownedBanknotes = coinDTO.OwnedCoins
                .Select(dto => new OwnedCoin
                {
                    Id = dto.Id != 0? dto.Id : DateTime.UtcNow.Ticks,
                    CoinId = coinDTO.Id,
                    NumberOfCoins = dto.NumberOfCoins,
                    Quality = dto.Quality,
                })
                .ToList();
            _ownedCoinRepository.UpdateByCoin(coinDTO.Id, ownedBanknotes);
            return coinDTO;
        }

        public int GetTotalCoinsNumber()
        {
            return _coinRepository.GetTotalCoinsNumber();
        }
    }
}
