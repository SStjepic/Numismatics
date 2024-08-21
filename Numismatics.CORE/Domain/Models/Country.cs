using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domain.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Bank { get; set; }
        public Date StartYear { get; set; }
        public Date EndYear { get; set; }

        public Country() { }
        public Country(int id, string name, string capital, string bank, Date startYear, Date endYear)
        {
            Id = id;
            Name = name;
            Capital = capital;
            Bank = bank;
            StartYear = startYear;
            EndYear = endYear;
        }
    }
}
