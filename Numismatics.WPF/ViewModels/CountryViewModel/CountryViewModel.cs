using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.CountryViewModel
{
    public class CountryViewModel
    {
        private CountryService _countryService;

        public CountryViewModel()
        {
            _countryService = new CountryService();
        }

        public CountryDTO? CreateCountry(CountryDTO countryDTO)
        {
            return _countryService.Create(countryDTO);
        }

        public CountryDTO? UpdateCountry(CountryDTO countryDTO)
        {
            return _countryService?.Update(countryDTO);

        }

        public CountryDTO? DeleteCountry(CountryDTO countryDTO)
        {
            return _countryService.Delete(countryDTO);
        }

        public List<CountryDTO> GetAllCountries()
        {
            return _countryService.GetAll();
        }
    }
}
