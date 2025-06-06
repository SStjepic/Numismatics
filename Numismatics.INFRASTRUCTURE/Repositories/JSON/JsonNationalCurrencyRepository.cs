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

        public NationalCurrency? Get(long currencyId)
        {
            var nationalCurrencies = GetAll();
            return nationalCurrencies.Find(nc => nc.CurrencyId == currencyId);
        }

        public List<NationalCurrency> GetAll()
        {
            return Load();
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
