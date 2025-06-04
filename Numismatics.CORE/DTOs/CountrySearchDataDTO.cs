using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class CountrySearchDataDTO
    {
        public string Name { get; set; }

        public CountrySearchDataDTO() { }

        public CountrySearchDataDTO(string name)
        {
            Name = name;
        }
    }
}
