using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domain.Models
{
    public class Currency
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string HunderthPartName { get; set; }
        public string Code { get; set; }

        public Currency() { }
        public Currency(long id, string name, string hunderthPartName, string code)
        {
            Id = id;
            Name = name;
            HunderthPartName = hunderthPartName;
            Code = code;
        }
        public override bool Equals(object? obj)
        {
            return obj is Currency currency &&
                   Id == currency.Id;
        }
    }
}
