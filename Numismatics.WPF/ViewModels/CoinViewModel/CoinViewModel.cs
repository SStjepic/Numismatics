using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.CoinViewModel
{
    public class CoinViewModel
    {
        private CoinService _coinService;
        public CoinViewModel() 
        {
            _coinService = new CoinService();
        }

        public CoinDTO? CreateCoin(CoinDTO coin)
        {
            return _coinService.Create(coin);
        }

        public CoinDTO? UpdateCoin(CoinDTO coinDTO)
        {
            return _coinService.Update(coinDTO);
        }

        
    }
}
