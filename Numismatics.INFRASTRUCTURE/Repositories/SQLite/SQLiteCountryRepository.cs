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
    public class SQLiteCountryRepository : ICountryRepository
    {
        private readonly SQLRepositoryContext _context;

        public SQLiteCountryRepository(SQLRepositoryContext context)
        {
            _context = context;
        }

        public Country? Create(Country entity)
        {
            _context.Countries.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Country? Delete(long id)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Id == id);
            if (country == null) return null;

            _context.Countries.Remove(country);
            _context.SaveChanges();
            return country;
        }

        public Country? Get(long id)
        {
            return _context.Countries.FirstOrDefault(c => c.Id == id);
        }

        public List<Country> GetAll()
        {
            return _context.Countries.ToList();
        }

        public int GetTotalCountriesNumber()
        {
            return _context.Countries.Count();
        }

        public Country? Update(Country entity)
        {
            var existing = _context.Countries.FirstOrDefault(c => c.Id == entity.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existing;
        }

        public List<Country> GetByPage(int pageNumber, int pageSize, CountrySearchDataDTO searchParams)
        {
            var query = _context.Countries.AsQueryable();

            if (searchParams != null)
            {
                if (!string.IsNullOrWhiteSpace(searchParams.Name))
                {
                    query = query.Where(c => c.Name.ToLower().Contains(searchParams.Name.ToLower()));
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
