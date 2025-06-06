using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Repositories;
using Numismatics.INFRASTRUCTURE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Numismatics.INFRASTRUCTURE.Repositories.JSON
{
    public class JsonCountryRepository : JSONRepository<Country>, ICountryRepository
    {
        public JsonCountryRepository(): base(new JSONSerialization())
        {
            SetFileName("CountryData.json");
        }
        public Country? Create(Country country)
        {
            var instances = GetAll();
            instances.Add(country);
            Save(instances);
            return country;
        }

        public Country? Delete(long countryId)
        {
            var countries = GetAll();
            var oldCountry = Get(countryId);
            if (oldCountry == null) { return null; }
            countries.Remove(oldCountry);
            Save(countries);
            return oldCountry;
        }

        public Country? Get(long id)
        {
            var countries = GetAll();
            return countries.Find(c => c.Id == id);
        }

        public List<Country> GetAll()
        {
            return Load();
        }

        public Country? Update(Country entity)
        {
            var countries = GetAll();
            countries.RemoveAll(c => c.Id == entity.Id);
            countries.Add(entity);
            Save(countries);
            return entity;
        }

        public int GetTotalCountriesNumber()
        {
            return this.GetAll().Count;
        }
    }
}
