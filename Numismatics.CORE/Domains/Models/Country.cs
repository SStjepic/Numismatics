using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Models
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Bank { get; set; }
        public Date StartYear { get; set; }
        public Date EndYear { get; set; }

        public Country() { }
        public Country(long id, string name, string capital, string bank, Date startYear, Date endYear)
        {
            Id = id;
            Name = name;
            Capital = capital;
            Bank = bank;
            StartYear = startYear;
            EndYear = endYear;
        }

        public override bool Equals(object? obj)
        {
            return obj is Country country &&
                   Id == country.Id;
        }
    }
}
