using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        public List<Country> GetByPage(int pageNumber, int pageSize, CountrySearchDataDTO searchParams);
        public int GetTotalCountriesNumber();
    }
}
