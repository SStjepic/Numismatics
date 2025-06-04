using Numismatics.CORE.DTOs;
using Numismatics.WPF.ViewModels.CountryViewModels;
using Numismatics.WPF.ViewModels.CurrencyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.NationalCurrencyViewModels
{
    public class NationalCurrencyDataViewModel
    {
        public long Id { get; set; }
        public CurrencyDataViewModel Currency { get; set; }
        public List<CountryDataViewModel> Countries { get; set; }
        public NationalCurrencyDataViewModel() 
        {
            Id = -1;
            Currency = new CurrencyDataViewModel();
            Countries = new List<CountryDataViewModel>();
        }
        public NationalCurrencyDataViewModel(long id, CurrencyDataViewModel currency, List<CountryDataViewModel> countries)
        {
            Id = id;
            Currency = currency;
            Countries = countries;
        }
    }
}
