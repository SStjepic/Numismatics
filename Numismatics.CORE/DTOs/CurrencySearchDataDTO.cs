using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class CurrencySearchDataDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public CurrencySearchDataDTO() { }

        public CurrencySearchDataDTO(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
