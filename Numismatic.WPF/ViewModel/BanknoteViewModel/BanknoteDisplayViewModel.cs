using Numismatic.WPF.ViewModels.BanknoteViewModel;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatic.WPF.ViewModel.BanknoteViewModel
{
    public class BanknoteDisplayViewModel
    {
        
        private readonly BanknoteService banknoteService;

        public BanknoteDisplayViewModel()
        {
            banknoteService = new BanknoteService();
        }
        public List<BanknoteDataViewModel> GetBanknotes(int pageNumber, int pageSize)
        {
            List<BanknoteDataViewModel> banknotes = new List<BanknoteDataViewModel>();
            foreach(BanknoteDTO banknoteDTO in banknoteService.GetByPage(pageNumber, pageSize))
            {
                banknotes.Add(new BanknoteDataViewModel(banknoteDTO));
            }
            return banknotes;
        }
    }
}
