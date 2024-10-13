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

        public BanknoteService()
        {
            _banknoteRepository = new BanknoteRepository();
        }

        public BanknoteDTO? Create(BanknoteDTO banknoteDTO)
        {
            _banknoteRepository.Create(banknoteDTO.ToBanknote());
            return banknoteDTO;
        }

        public BanknoteDTO? Delete(BanknoteDTO banknoteDTO)
        {
            _banknoteRepository.Delete(banknoteDTO.Id);
            return banknoteDTO;
        }

        public BanknoteDTO? Get(int entityId)
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

        public List<BanknoteDTO> GetByPage(int pageNumber, int pageSize)
        {
            var banknotes = _banknoteRepository.GetAll();
            var currentBanknotes = new List<BanknoteDTO>();
            var selectedBanknotes = banknotes.Skip((pageNumber-1) * pageSize).Take(pageSize).ToList();
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

        public BanknoteDTO? Update(BanknoteDTO banknoteDTO)
        {
            _banknoteRepository.Update(banknoteDTO.ToBanknote());
            return banknoteDTO;
        }
    }
}
