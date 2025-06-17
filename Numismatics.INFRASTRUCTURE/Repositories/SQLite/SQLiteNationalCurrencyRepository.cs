using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.INFRASTRUCTURE.Repositories.SQLite
{
    public class SQLiteNationalCurrencyRepository : INationalCurrencyRepository
    {
        private readonly SQLRepositoryContext _context;

        public SQLiteNationalCurrencyRepository(SQLRepositoryContext context)
        {
            _context = context;
        }

        public NationalCurrency? Create(NationalCurrency entity)
        {
            _context.Nationalcurrencies.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public NationalCurrency? Delete(long id)
        {
            var nc = _context.Nationalcurrencies.FirstOrDefault(n => n.Id == id);
            if (nc == null) return null;

            _context.Nationalcurrencies.Remove(nc);
            _context.SaveChanges();
            return nc;
        }

        public NationalCurrency? Get(long id)
        {
            return _context.Nationalcurrencies.FirstOrDefault(n => n.Id == id);
        }

        public List<NationalCurrency> GetAll()
        {
            return _context.Nationalcurrencies.ToList();
        }

        public List<NationalCurrency> GetByCurrency(long currencyId)
        {
            return _context.Nationalcurrencies
                .Where(n => n.CurrencyId == currencyId)
                .ToList();
        }

        public NationalCurrency? Update(NationalCurrency entity)
        {
            var existing = _context.Nationalcurrencies.FirstOrDefault(n => n.Id == entity.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existing;
        }
    }

}
