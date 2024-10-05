using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public class CountryRepository : Repository<Country>, IRepository<Country>
    {
        public CountryRepository() 
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

        public Country? Delete(int countryId)
        {
            var countries = GetAll();
            var oldCountry = Get(countryId);
            if(oldCountry == null) { return null; }
            countries.Remove(oldCountry);
            Save(countries);
            return oldCountry;
        }

        public Country? Get(int id)
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
    }
}
