using Numismatics.CORE.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Repositories;

namespace Numismatics.CORE.Services.CountryService
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
            countryDTO.Id = HashCode.Combine(countryDTO.Name, countryDTO.StartYear);
            _countryRepository.Create(countryDTO.ToCountry());
            return countryDTO;
        }

        public CountryDTO? Delete(CountryDTO entity)
        {
            throw new NotImplementedException();
        }

        public CountryDTO? Get(CountryDTO entity)
        {
            throw new NotImplementedException();
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

        public CountryDTO? Update(CountryDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
