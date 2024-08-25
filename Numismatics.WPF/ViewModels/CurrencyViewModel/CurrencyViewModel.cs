using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.CurrencyViewModel
{
    public class CurrencyViewModel
    {
        private CurrencyService _currencyService;
        public CurrencyViewModel() 
        {
            _currencyService = new CurrencyService();
        }

        public CurrencyDTO? CreateCurrency(CurrencyDTO currencyDTO)
        {
            return _currencyService.Create(currencyDTO);
        }
        public CurrencyDTO? UpdateCurrency(CurrencyDTO currencyDTO)
        {
            return _currencyService.Update(currencyDTO);
        }
        public CurrencyDTO? DeleteCurrency(CurrencyDTO currencyDTO)
        {
            return _currencyService.Delete(currencyDTO);
        }
    }
}
