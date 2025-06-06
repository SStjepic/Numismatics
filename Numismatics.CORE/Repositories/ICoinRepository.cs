using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface ICoinRepository: IRepository<Coin>
    {
        public List<Coin> GetByPage(int pageNumber, int pageSize, CoinSearchDataDTO searchParams);
        public int GetTotalCoinsNumber();
    }
}
