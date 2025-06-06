using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Interface
{
    public interface IBanknoteService: IService<BanknoteDTO>
    {
        List<BanknoteDTO> GetByPage(int pageNumber, int pageSize, BanknoteSearchDataDTO searchData);
        int GetTotalBanknotesNumber();
    }
}
