using Numismatics.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModel.CountryViewModel
{
    public class CountrySearchDataViewModel
    {
        public string Name { get; set; }

        public CountrySearchDataViewModel() { }

        public CountrySearchDataViewModel(string name)
        { 
            Name = name;
        }
        public CountrySearchDataDTO ToCountrySearchDataDTO()
        {
            return new CountrySearchDataDTO(Name);
        }
    }
}
