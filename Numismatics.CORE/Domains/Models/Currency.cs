using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Models
{
    public class Currency
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SubunitName { get; set; }
        public string MainUnitName { get; set; }
        public int SubunitToMainUnit { get; set; }
        public string? Code { get; set; }

        public Currency() { }

        public Currency(long id, string name, string mainUnitName, string subunitName, int subunitToMainUnit, string code)
        {
            Id = id;
            Name = name;
            SubunitName = subunitName;
            MainUnitName = mainUnitName;
            SubunitToMainUnit = subunitToMainUnit;
            Code = code;
        }

        public override bool Equals(object? obj)
        {
            return obj is Currency currency &&
                   Id == currency.Id;
        }
    }
}
