using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface ICurrencyRepository: IRepository<Currency>
    {
        public int GetTotalCurrenciesNumber();
    }
}
