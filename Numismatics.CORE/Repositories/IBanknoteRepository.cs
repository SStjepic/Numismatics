using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface IBanknoteRepository: IRepository<Banknote>
    {
        public int GetTotalBanknotesNumber();
    }
}
