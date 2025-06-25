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
        private IOwnedBanknotesRepository _ownedBanknotesRepository;

        public BanknoteService(IBanknoteRepository banknoteRepository, IImageRepository imageRepository, ICurrencyRepository currencyRepository, ICountryRepository countryRepository, IOwnedBanknotesRepository ownedBanknotesRepository)
        {
            _banknoteRepository = banknoteRepository;
            _imageRepository = imageRepository;
            _currencyRepository = currencyRepository;
            _countryRepository = countryRepository;
            _ownedBanknotesRepository = ownedBanknotesRepository;
        }

        public BanknoteDTO? Create(BanknoteDTO banknoteDTO)
        {
            banknoteDTO.Id = DateTime.UtcNow.Ticks;
            (banknoteDTO.ObversePicture, banknoteDTO.ReversePicture) = _imageRepository.SaveBanknoteImage(banknoteDTO.Id, banknoteDTO.ObversePicture, banknoteDTO.ReversePicture);
            _banknoteRepository.Create(banknoteDTO.ToBanknote());
            foreach (OwnedBanknoteDTO ownedBanknote in banknoteDTO.OwnedBanknotes) 
            {
                ownedBanknote.Id = DateTime.UtcNow.Ticks;
                ownedBanknote.BanknoteId = banknoteDTO.Id;
                _ownedBanknotesRepository.Create(ownedBanknote.ToOwnedBanknote());
            }
            return banknoteDTO;
        }

        public BanknoteDTO? Update(BanknoteDTO banknoteDTO)
        {
            (banknoteDTO.ObversePicture, banknoteDTO.ReversePicture) = _imageRepository.SaveBanknoteImage(banknoteDTO.Id, banknoteDTO.ObversePicture, banknoteDTO.ReversePicture);
            _banknoteRepository.Update(banknoteDTO.ToBanknote());
            var ownedBanknotes = banknoteDTO.OwnedBanknotes
                .Select(dto => new OwnedBanknote
                {
                    Id = dto.Id,
                    BanknoteId = banknoteDTO.Id,
                    Code = dto.Code,
                    Quality = dto.Quality,
                })
                .ToList();
            _ownedBanknotesRepository.UpdateByBanknote(banknoteDTO.Id, ownedBanknotes);
            return banknoteDTO;
        }

        public BanknoteDTO? Delete(BanknoteDTO banknoteDTO)
        {
            _imageRepository.DeleteBanknote(banknoteDTO.Id);
            _banknoteRepository.Delete(banknoteDTO.Id);
            foreach (OwnedBanknoteDTO ownedBanknote in banknoteDTO.OwnedBanknotes)
            {
                _ownedBanknotesRepository.Delete(ownedBanknote.ToOwnedBanknote().Id);
            }
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
                var ownedBanknote = _ownedBanknotesRepository.GetByBanknote(banknote.Id);
                var country = countries.Find(c => c.Id == banknote.CountryId);
                var currency = currencies.Find(c => c.Id == banknote.CurrencyId);
                banknoteDTOs.Add(new BanknoteDTO(banknote,country, currency, ToOwnedBanknotes(ownedBanknote)));
            }
            return banknoteDTOs;
        }

        private List<OwnedBanknoteDTO> ToOwnedBanknotes(List<OwnedBanknote> ownedBanknotes)
        {
            var DTOs = new List<OwnedBanknoteDTO>();
            foreach(var banknote in ownedBanknotes)
            {
                DTOs.Add(new OwnedBanknoteDTO(banknote));
            }
            return DTOs;
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
                var ownedBanknote = _ownedBanknotesRepository.GetByBanknote(banknote.Id);
                var country = countries.Find(c => c.Id == banknote.CountryId);
                var currency = currencies.Find(c => c.Id == banknote.CurrencyId);
                currentBanknotes.Add(new BanknoteDTO(banknote, country, currency, ToOwnedBanknotes(ownedBanknote)));
            }

            return currentBanknotes;
        }


        public int GetTotalBanknotesNumber()
        {
            return this._banknoteRepository.GetTotalBanknotesNumber();
        }
    }
}
