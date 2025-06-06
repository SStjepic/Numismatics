using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface IBanknoteRepository: IRepository<Banknote>
    {
        List<Banknote> GetByPage(int pageNumber, int pageSize, BanknoteSearchDataDTO searchParams);
        public int GetTotalBanknotesNumber();
    }
}
