using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface IImageRepository
    {
        public (string obverseImagePath, string reverseImagePath) SaveCoinImage(long moneyId, string obversePath, string reversePath);

        public (string obverseImagePath, string reverseImagePath) SaveBanknoteImage(long moneyId, string obversePath, string reversePath);
        public void DeleteBanknote(long id);
        public void DeleteCoin(long id);
    }
}
