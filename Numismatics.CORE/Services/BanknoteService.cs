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
    public class BanknoteService: IService<BanknoteDTO>
    {
        private BanknoteRepository _banknoteRepository;
        private ImageRepository _imageRepository;

        public BanknoteService()
        {
            _banknoteRepository = new BanknoteRepository();
            _imageRepository = new ImageRepository();
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
            CurrencyRepository _currencyRepository = new CurrencyRepository();
            CountryRepository _countryRepository = new CountryRepository();
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


        public List<BanknoteDTO> GetByPage(int pageNumber, int pageSize, object param)
        {
            var banknotes = _banknoteRepository.GetAll();

            var searchParams = param as BanknoteSearchDataDTO;
            if (searchParams != null)
            {
                if (searchParams.Value > 0)
                {
                    banknotes = banknotes
                        .Where(b => b.Value == searchParams.Value)
                        .ToList();
                }

                if (searchParams.Year > 0)
                {
                    banknotes = banknotes
                        .Where(b => b.IssueDate.Year == searchParams.Year)
                        .ToList();
                }

                if (searchParams.Country.Id > 0)
                {
                    banknotes = banknotes
                        .Where(b => b.CountryId == searchParams.Country.Id)
                        .ToList();
                }

                if (searchParams.Currency.Id > 0)
                {
                    banknotes = banknotes
                        .Where(b => b.CurrencyId == searchParams.Currency.Id)
                        .ToList();
                }
            }

            banknotes = banknotes
                .OrderByDescending(b => b.IssueDate)
                .ThenBy(b => b.IsSubunit)
                .ThenByDescending(b => b.Value)
                .ToList();

            var selectedBanknotes = banknotes.Skip(pageNumber * pageSize).Take(pageSize).ToList();

            var currentBanknotes = new List<BanknoteDTO>();
            CurrencyRepository _currencyRepository = new CurrencyRepository();
            CountryRepository _countryRepository = new CountryRepository();
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
