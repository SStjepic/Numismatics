using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class CountryDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Bank { get; set; }
        public Date StartYear { get; set; }
        public Date EndYear { get; set; }

        public CountryDTO() { }
        public CountryDTO(Country country)
        {
            if (country != null)
            {
                Id = country.Id;
                Name = country.Name;
                Capital = country.Capital;
                Bank = country.Bank;
                StartYear = country.StartYear;
                EndYear = country.EndYear;
            }
            else
            {
                Id = -1;
            }
        }

        public CountryDTO(long id, string name, string capital, string bank, Date startYear, Date endYear)
        {
            Id = id;
            Name = name;
            Capital = capital;
            Bank = bank;
            StartYear = startYear;
            EndYear = endYear;
        }

        public Country ToCountry()
        {
            return new Country(Id, Name, Capital, Bank, StartYear, EndYear);
        }

        public override bool Equals(object? obj)
        {
            return obj is CountryDTO dTO &&
                   Id == dTO.Id;
        }
    }
}
