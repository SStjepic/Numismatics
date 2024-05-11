using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics
{
    public abstract class MoneyItem
    {
        public int Id {  get; set; }
        public double Denomination {  get; set; }
        public string Details { get; set; }
        public Country Country { get; set; }
        public Currency Currency { get; set; }

        protected MoneyItem(int id, double denomination, string details, Country country, Currency currency)
        {
            Id = id;
            Denomination = denomination;
            Details = details;
            Country = country;
            Currency = currency;
        }



    }
}
