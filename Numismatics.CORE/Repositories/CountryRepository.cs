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

        public Country? Delete(Country entity)
        {
            throw new NotImplementedException();
        }

        public Country? Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Country> GetAll()
        {
            return Load();
        }

        public Country? Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
