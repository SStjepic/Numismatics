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
    public class SQLiteCoinRepository : ICoinRepository
    {
        private readonly SQLRepositoryContext _context;

        public SQLiteCoinRepository(SQLRepositoryContext context)
        {
            _context = context;
        }

        public Coin? Create(Coin entity)
        {
            _context.Coins.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Coin? Delete(long id)
        {
            var coin = _context.Coins.FirstOrDefault(c => c.Id == id);
            if (coin == null) return null;

            _context.Coins.Remove(coin);
            _context.SaveChanges();
            return coin;
        }

        public Coin? Get(long id)
        {
            return _context.Coins.FirstOrDefault(c => c.Id == id);
        }

        public List<Coin> GetAll()
        {
            return _context.Coins.ToList();
        }

        public int GetTotalCoinsNumber()
        {
            return _context.Coins.Count();
        }

        public Coin? Update(Coin entity)
        {
            var existing = _context.Coins.FirstOrDefault(c => c.Id == entity.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existing;
        }

        public List<Coin> GetByPage(int pageNumber, int pageSize, CoinSearchDataDTO searchParams)
        {
            var query = _context.Coins.AsQueryable();

            if (searchParams != null)
            {
                if (searchParams.Value > 0)
                    query = query.Where(c => c.Value == searchParams.Value);

                if (searchParams.Year > 0)
                    query = query.Where(c => c.IssueDate.Year == searchParams.Year);

                if (searchParams.Country?.Id > 0)
                    query = query.Where(c => c.CountryId == searchParams.Country.Id);

                if (searchParams.Currency?.Id > 0)
                    query = query.Where(c => c.CurrencyId == searchParams.Currency.Id);
            }

            query = query
                .OrderByDescending(c => c.IssueDate)
                .ThenBy(c => c.IsSubunit)
                .ThenByDescending(c => c.Value);

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }

}
