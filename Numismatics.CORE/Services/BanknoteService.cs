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
    public class BanknoteService: IBanknoteService
    {
        private IBanknoteRepository _banknoteRepository;
        private IImageRepository _imageRepository;
        private ICurrencyRepository _currencyRepository;
        private ICountryRepository _countryRepository;

        public BanknoteService(IBanknoteRepository banknoteRepository, IImageRepository imageRepository, ICurrencyRepository currencyRepository, ICountryRepository countryRepository)
        {
            _banknoteRepository = banknoteRepository;
            _imageRepository = imageRepository;
            _currencyRepository = currencyRepository;
            _countryRepository = countryRepository;
        }

        public BanknoteDTO? Create(BanknoteDTO banknoteDTO)
        {
            banknoteDTO.Id = DateTime.UtcNow.Ticks;
            (banknoteDTO.ObversePicture, banknoteDTO.ReversePicture) = _imageRepository.SaveBanknoteImage(banknoteDTO.Id, banknoteDTO.ObversePicture, banknoteDTO.ReversePicture);
            _banknoteRepository.Create(banknoteDTO.ToBanknote());
            return banknoteDTO;
        }

        public BanknoteDTO? Update(BanknoteDTO banknoteDTO)
        {
            (banknoteDTO.ObversePicture, banknoteDTO.ReversePicture) = _imageRepository.SaveBanknoteImage(banknoteDTO.Id, banknoteDTO.ObversePicture, banknoteDTO.ReversePicture);
            _banknoteRepository.Update(banknoteDTO.ToBanknote());
            return banknoteDTO;
        }

        public BanknoteDTO? Delete(BanknoteDTO banknoteDTO)
        {
            _imageRepository.DeleteBanknote(banknoteDTO.Id);
            _banknoteRepository.Delete(banknoteDTO.Id);
            return banknoteDTO;
        }

        public BanknoteDTO? Get(long entityId)
        {
            throw new NotImplementedException();
        }

        public List<BanknoteDTO> GetAll()
        {
            var banknoteDTOs = new List<BanknoteDTO>();
            var banknotes = _banknoteRepository.GetAll();
            var countries = _countryRepository.GetAll();
            var currencies = _currencyRepository.GetAll();
            foreach (var banknote in banknotes)
            {
                var country = countries.Find(c => c.Id == banknote.CountryId);
                var currency = currencies.Find(c => c.Id == banknote.CurrencyId);
                banknoteDTOs.Add(new BanknoteDTO(banknote,country, currency));
            }
            return banknoteDTOs;
        }

        public int GetTotalPageNumber(int pageSize)
        {
            var totalItems = _banknoteRepository.GetAll().Count();
            return Math.Max(1,(int)Math.Ceiling((double)totalItems / pageSize));
        }


        public List<BanknoteDTO> GetByPage(int pageNumber, int pageSize, BanknoteSearchDataDTO searchParams)
        {
            var selectedBanknotes = _banknoteRepository.GetByPage(pageNumber, pageSize, searchParams);
            var currentBanknotes = new List<BanknoteDTO>();
            
            var countries = _countryRepository.GetAll();
            var currencies = _currencyRepository.GetAll();

            foreach (var banknote in selectedBanknotes)
            {
                var country = countries.Find(c => c.Id == banknote.CountryId);
                var currency = currencies.Find(c => c.Id == banknote.CurrencyId);
                currentBanknotes.Add(new BanknoteDTO(banknote, country, currency));
            }

            return currentBanknotes;
        }


        public int GetTotalBanknotesNumber()
        {
            return this._banknoteRepository.GetTotalBanknotesNumber();
        }
    }
}
