using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Interface
{
    public interface ICountryService: IService<CountryDTO>
    {
        public List<CountryDTO> GetByPage(int pageNumber, int pageSize, CountrySearchDataDTO param);
        public int GetTotalCountriesNumber();
    }
}
