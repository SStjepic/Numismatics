using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTO
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HunderthPartName { get; set; }
        public string Code { get; set; }
        public CurrencyDTO() { }
        public CurrencyDTO(Currency currency)
        {
            Id = currency.Id;
            Name = currency.Name;
            HunderthPartName = currency.HunderthPartName;
            Code = currency.Code;
        }
        public CurrencyDTO(int id, string name, string hunderthPartName, string code)
        {
            Id = id;
            Name = name;
            HunderthPartName = hunderthPartName;
            Code = code;
        }

        public Currency ToCurrency()
        {
            return new Currency(Id, Name, HunderthPartName, Code);
        }
    }
}
