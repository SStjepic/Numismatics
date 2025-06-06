using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
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
    public class JsonCurrencyRepository : JSONRepository<Currency>, ICurrencyRepository
    {
        public JsonCurrencyRepository() : base(new JSONSerialization())
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
            if (oldCurrency == null) { return null; }
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

        public List<Currency> GetByPage(int pageNumber, int pageSize, CurrencySearchDataDTO searchParams)
        {
            var currencies = this.GetAll().AsEnumerable();

            if (searchParams != null)
            {
                if (!string.IsNullOrEmpty(searchParams.Name))
                {
                    currencies = currencies
                        .Where(c => c.Name.ToLower().Contains(searchParams.Name.ToLower()));
                }

                if (!string.IsNullOrEmpty(searchParams.Code))
                {
                    currencies = currencies
                        .Where(c => !string.IsNullOrEmpty(c.Code) && c.Code.ToLower().Contains(searchParams.Code.ToLower()));
                }
            }
            return currencies.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
    }
}
