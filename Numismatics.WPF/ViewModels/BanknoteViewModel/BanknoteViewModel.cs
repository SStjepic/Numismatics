using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.BanknoteViewModel
{
    public class BanknoteViewModel
    {
        private BanknoteService _banknoteService;
        public BanknoteViewModel()
        {
            _banknoteService = new BanknoteService();
        }

        public BanknoteDTO? CreateBanknote(BanknoteDTO banknote)
        {
            return _banknoteService.Create(banknote);
        }

        public BanknoteDTO? UpdateBanknote(BanknoteDTO banknote)
        {
            return _banknoteService.Update(banknote);
        }
    }
}
