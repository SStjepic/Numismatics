using Numismatics.CORE.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Repositories;

namespace Numismatics.CORE.Services
{
    public class CountryService : IService<CountryDTO>
    {
        private CountryRepository _countryRepository;

        public CountryService()
        {
            _countryRepository = new CountryRepository();
        }
        public CountryDTO? Create(CountryDTO countryDTO)
        {
            countryDTO.Id = DateTime.UtcNow.Ticks;
            _countryRepository.Create(countryDTO.ToCountry());
            return countryDTO;
        }

        public CountryDTO? Delete(CountryDTO entity)
        {
            _countryRepository.Delete(entity.Id);
            return entity;
        }

        public CountryDTO? Get(long countryId)
        {
            return new CountryDTO(_countryRepository.Get(countryId));
        }

        public List<CountryDTO> GetAll()
        {
            var countries = _countryRepository.GetAll();
            var countriesDTO = new List<CountryDTO>();
            foreach (var country in countries)
            {
                var countryDTO = new CountryDTO(country);
                countriesDTO.Add(countryDTO);
            }
            return countriesDTO;
        }

        public List<CountryDTO> GetByPage(int pageNumber, int pageSize, object param)
        {
            var countryDTOs = new List<CountryDTO>();
            var countries = _countryRepository.GetAll();
            
            var searchParams = param as CountrySearchDataDTO;
            if(searchParams != null)
            {
                if (!string.IsNullOrEmpty(searchParams.Name))
                {
                    countries = countries.Where(c => c.Name.ToLower().Contains(searchParams.Name.ToLower()))
                        .ToList();
                }
            }
            var selectedCountries = countries.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            foreach (var counrty in selectedCountries)
            {
                countryDTOs.Add(new CountryDTO(counrty));
            }

            return countryDTOs;
        }

        public int GetTotalPageNumber(int pageSize)
        {
            var totalItems = _countryRepository.GetAll().Count();
            return Math.Max(1, (int)Math.Ceiling((double)totalItems / pageSize));
        }

        public CountryDTO? Update(CountryDTO entity)
        {
            _countryRepository.Update(entity.ToCountry());
            return entity;
        }

        public int GetTotalCountriesNumber()
        {
            return _countryRepository.GetTotalCountriesNumber();
        }
    }
}
