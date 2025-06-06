using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface ICurrencyRepository: IRepository<Currency>
    {
        public List<Currency> GetByPage(int pageNumber, int pageSize, CurrencySearchDataDTO searchParams);
        public int GetTotalCurrenciesNumber();
    }
}
