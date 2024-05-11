using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics
{
    public class Banknote: MoneyItem
    {
        public DateTime ReleaseDate {  get; set; }
        public Dictionary<string, BanknoteQuality> Items { get; set; }

        public Banknote(int id, double denomination, string details, Country country, Currency currency, DateTime releaseDate, Dictionary<string, BanknoteQuality> items) 
            : base(id, denomination, details, country, currency)
        {
            ReleaseDate = releaseDate;
            Items = items;
        }


    }
}
