using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public class NationalCurrencyRepository : Repository<NationalCurrency>, IRepository<NationalCurrency>
    {
        public NationalCurrencyRepository() 
        {
            SetFileName("NationalCurrencyData.json");
        }
        public NationalCurrency? Create(NationalCurrency entity)
        {
            var nationalCurrencies = Load();
            nationalCurrencies.Add(entity);
            Save(nationalCurrencies);
            return entity;
        }

        public NationalCurrency? Delete(NationalCurrency entity)
        {
            var nationalCurrencies = Load();
            nationalCurrencies.Remove(entity);
            Save(nationalCurrencies);
            return entity;
        }

        public NationalCurrency? Get(int id)
        {
            var nationalCurrencies = Load();
            return nationalCurrencies.Find(nc => nc.Id == id);
        }

        public List<NationalCurrency> GetAll()
        {
            return Load();
        }

        public NationalCurrency? Update(NationalCurrency entity)
        {
            var nationalCurrencies = Load();
            var oldEntity = Get(entity.Id);
            nationalCurrencies.Remove(oldEntity);
            nationalCurrencies.Add(entity);
            Save(nationalCurrencies);
            return entity;
        }
    }
}
