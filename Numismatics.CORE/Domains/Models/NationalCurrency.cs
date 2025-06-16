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
        public long CountryId;

        public NationalCurrency()
        {
        }

        public NationalCurrency(long id, long currencyId, long countryId)
        {
            Id = id;
            CurrencyId = currencyId;
            CountryId = countryId;
        }

        public override bool Equals(object? obj)
        {
            return obj is NationalCurrency nationalCurrency &&
                   Id == nationalCurrency.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CurrencyId, CountryId);
        }
    }
}
