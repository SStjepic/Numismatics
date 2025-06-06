using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Interface
{
    public interface ICurrencyService: IService<CurrencyDTO>
    {
        public List<CurrencyDTO> GetByPage(int pageNumber, int pageSize, CurrencySearchDataDTO searchParams);
        public int GetTotalCurrenciesNumber();
    }
}
