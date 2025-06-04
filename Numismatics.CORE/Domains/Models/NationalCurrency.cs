using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Models
{
    public class NationalCurrency
    {
        public long Id { get; set; } 
        public long CurrencyId;
        public List<long> Countries;

        public NationalCurrency()
        {
        }

        public NationalCurrency(long id, long currencyId, List<long> countries)
        {
            Id = id;
            CurrencyId = currencyId;
            Countries = countries;
        }

        public override bool Equals(object? obj)
        {
            return obj is NationalCurrency currency &&
                   Id == currency.Id;
        }
    }
}
