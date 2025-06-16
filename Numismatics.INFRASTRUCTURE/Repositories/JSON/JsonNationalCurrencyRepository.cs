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
    public class JsonNationalCurrencyRepository : JSONRepository<NationalCurrency>, INationalCurrencyRepository
    {
        public JsonNationalCurrencyRepository() : base(new JSONSerialization())
        {
            SetFileName("NationalCurrencyData.json");
        }
        public NationalCurrency? Create(NationalCurrency entity)
        {
            var nationalCurrencies = GetAll();
            nationalCurrencies.Add(entity);
            Save(nationalCurrencies);
            return entity;
        }

        public NationalCurrency? Delete(long NationalCurrencyId)
        {
            var nationalCurrencies = GetAll();
            var oldNationalCurrency = Get(NationalCurrencyId);
            if (oldNationalCurrency == null) { return null; }
            nationalCurrencies.Remove(oldNationalCurrency);
            Save(nationalCurrencies);
            return oldNationalCurrency;
        }

        public NationalCurrency? Get(long id)
        {
            return GetAll().Find(nc => nc.Id == id);
        }

        public List<NationalCurrency> GetAll()
        {
            return Load();
        }

        public List<NationalCurrency> GetByCurrency(long currencyId)
        {
            return Load().Where(nc => nc.CurrencyId == currencyId).ToList();
        }

        public NationalCurrency? Update(NationalCurrency entity)
        {
            var nationalCurrencies = GetAll();
            var oldEntity = Get(entity.Id);
            nationalCurrencies.Remove(oldEntity);
            nationalCurrencies.Add(entity);
            Save(nationalCurrencies);
            return entity;
        }
    }
}
