using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface IOwnedBanknotesRepository : IRepository<OwnedBanknote>
    {
        public List<OwnedBanknote> GetByBanknote(long banknoteId);
        public List<OwnedBanknote> UpdateByBanknote(long banknoteId, List<OwnedBanknote> banknotes);
    }
}
