using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface IOwnedCoinRepository: IRepository<OwnedCoin>
    {
        public List<OwnedCoin> GetByCoin(long coinId);
        public List<OwnedCoin> UpdateByCoin(long coinId, List<OwnedCoin> coins);
    }
}
