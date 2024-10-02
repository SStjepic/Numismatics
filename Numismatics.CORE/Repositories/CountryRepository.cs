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
            var instances = Load();
            instances.Add(country);
            Save(instances);
            return country;
        }

        public Country? Delete(Country oldCountry)
        {
            var countries = Load();
            var oldInstance = Get(oldCountry.Id);
            countries.Remove(oldInstance);
            Save(countries);
            return oldCountry;
        }

        public Country? Get(int id)
        {
            var countries = Load();
            return countries.Find(c => c.Id == id);
        }

        public List<Country> GetAll()
        {
            return Load();
        }

        public Country? Update(Country entity)
        {
            var countries = Load();
            countries.RemoveAll(c => c.Id == entity.Id);
            countries.Add(entity);
            Save(countries);
            return entity;
        }
    }
}
