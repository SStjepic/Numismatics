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
    public class SQLiteBanknoteRepository : IBanknoteRepository
    {
        private readonly SQLRepositoryContext _context;

        public SQLiteBanknoteRepository(SQLRepositoryContext context)
        {
            _context = context;
        }

        public Banknote? Create(Banknote entity)
        {
            _context.Banknotes.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Banknote? Delete(long id)
        {
            var banknote = _context.Banknotes.FirstOrDefault(b => b.Id == id);
            if (banknote == null) return null;

            _context.Banknotes.Remove(banknote);
            _context.SaveChanges();
            return banknote;
        }

        public Banknote? Get(long id)
        {
            return _context.Banknotes.FirstOrDefault(b => b.Id == id);
        }

        public List<Banknote> GetAll()
        {
            return _context.Banknotes.ToList();
        }

        public List<Banknote> GetByPage(int pageNumber, int pageSize, BanknoteSearchDataDTO searchParams)
        {
            var query = _context.Banknotes.AsQueryable();

            if (searchParams != null)
            {
                if (searchParams.Value > 0)
                {
                    query = query.Where(b => b.Value == searchParams.Value);
                }

                if (searchParams.Year > 0)
                {
                    query = query.Where(b => b.IssueDate.Year == searchParams.Year);
                }

                if (searchParams.Country?.Id > 0)
                {
                    query = query.Where(b => b.CountryId == searchParams.Country.Id);
                }

                if (searchParams.Currency?.Id > 0)
                {
                    query = query.Where(b => b.CurrencyId == searchParams.Currency.Id);
                }
            }

            query = query
                .OrderByDescending(b => b.IssueDate)
                .ThenBy(b => b.IsSubunit)
                .ThenByDescending(b => b.Value);

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


        public int GetTotalBanknotesNumber()
        {
            return _context.Banknotes.Count();
        }

        public Banknote? Update(Banknote entity)
        {
            var existing = _context.Banknotes.FirstOrDefault(b => b.Id == entity.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existing;
        }
    }

}
