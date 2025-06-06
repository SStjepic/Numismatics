using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Interface
{
    public interface ICoinService: IService<CoinDTO>
    {
        public List<CoinDTO> GetByPage(int pageNumber, int pageSize, CoinSearchDataDTO param);
        public int GetTotalCoinsNumber();
    }
}
