using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
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
            var currencies = GetAll();
            currencies.Add(entity);
            Save(currencies);
            return entity;
        }

        public Currency? Delete(long currencyId)
        {
            var currencies = GetAll();
            var oldCurrency = Get(currencyId);
            if(oldCurrency == null) { return null; }
            currencies.Remove(oldCurrency);
            Save(currencies);
            return oldCurrency;
        }

        public Currency? Get(long id)
        {
            var currencies = GetAll();
            return currencies.Find(c => c.Id == id);
        }

        public List<Currency> GetAll()
        {
            return Load();
        }

        public Currency? Update(Currency entity)
        {
            var currencies = GetAll();
            currencies.RemoveAll(c => c.Id == entity.Id);
            currencies.Add(entity);
            Save(currencies);
            return entity;
        }

        public int GetTotalCurrenciesNumber()
        {
            return this.GetAll().Count();
        }
    }
}
