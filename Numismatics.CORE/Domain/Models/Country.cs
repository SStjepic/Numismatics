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
        public Date StartDate { get; set; }
        public Date EndDate { get; set; }

        public Country(int id, string name, string capital, string bank, Date startDate, Date endDate)
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
