using Newtonsoft.Json.Linq;
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModel.CurrencyViewModel
{
    public class CurrencySearchDataViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public CurrencySearchDataViewModel() { }
        public CurrencySearchDataViewModel(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public CurrencySearchDataDTO ToCurrencySearchDataDTO()
        {
            return new CurrencySearchDataDTO(Name, Code);
        }
    }
}
