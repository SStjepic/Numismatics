using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public class CurrencyRepository : Repository<Currency>, IRepository<Currency>
    {
        public CurrencyRepository() 
        {
            SetFileName("CurrencyData.json");
        }
        public Currency? Create(Currency entity)
        {
            var currencies = Load();
            currencies.Add(entity);
            Save(currencies);
            return entity;
        }

        public Currency? Delete(Currency entity)
        {
            throw new NotImplementedException();
        }

        public Currency? Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Currency> GetAll()
        {
            return Load();
        }

        public Currency? Update(Currency entity)
        {
            var currencies = Load();
            currencies.RemoveAll(c => c.Id == entity.Id);
            currencies.Add(entity);
            Save(currencies);
            return entity;
        }
    }
}
