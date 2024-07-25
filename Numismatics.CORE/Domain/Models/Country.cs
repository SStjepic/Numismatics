using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Bank { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}

        public Country(int id, string name, string capital, string bank, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            Capital = capital;
            Bank = bank;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
