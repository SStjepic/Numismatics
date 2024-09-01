using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domain.Models
{
    public class NationalCurrency
    {
        public int Id { get; set; } 
        public int CurrencyId;
        public List<int> Countries;

        public NationalCurrency()
        {
        }

        public NationalCurrency(int id, int currencyId, List<int> countries)
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
