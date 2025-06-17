using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
using Numismatics.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.INFRASTRUCTURE.Repositories.SQLite
{
    public class SQLiteCurrencyRepository : ICurrencyRepository
    {
        private readonly SQLRepositoryContext _context;

        public SQLiteCurrencyRepository(SQLRepositoryContext context)
        {
            _context = context;
        }

        public Currency? Create(Currency entity)
        {
            _context.Currencies.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Currency? Delete(long id)
        {
            var currency = _context.Currencies.FirstOrDefault(c => c.Id == id);
            if (currency == null) return null;

            _context.Currencies.Remove(currency);
            _context.SaveChanges();
            return currency;
        }

        public Currency? Get(long id)
        {
            return _context.Currencies.FirstOrDefault(c => c.Id == id);
        }

        public List<Currency> GetAll()
        {
            return _context.Currencies.ToList();
        }

        public int GetTotalCurrenciesNumber()
        {
            return _context.Currencies.Count();
        }

        public Currency? Update(Currency entity)
        {
            var existing = _context.Currencies.FirstOrDefault(c => c.Id == entity.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existing;
        }

        public List<Currency> GetByPage(int pageNumber, int pageSize, CurrencySearchDataDTO searchParams)
        {
            var query = _context.Currencies.AsQueryable();

            if (searchParams != null)
            {
                if (!string.IsNullOrWhiteSpace(searchParams.Name))
                {
                    query = query.Where(c => c.Name.ToLower().Contains(searchParams.Name.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(searchParams.Code))
                {
                    query = query.Where(c => c.Code.ToLower().Contains(searchParams.Code.ToLower()));
                }
            }

            query = query.OrderBy(c => c.Name);

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }

}
