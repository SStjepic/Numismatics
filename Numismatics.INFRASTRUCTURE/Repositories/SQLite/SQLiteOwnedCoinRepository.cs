using Microsoft.EntityFrameworkCore;
using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Numismatics.INFRASTRUCTURE.Repositories.SQLite
{
    public class SQLiteOwnedCoinRepository : IOwnedCoinRepository
    {
        private readonly SQLRepositoryContext _context;

        public SQLiteOwnedCoinRepository(SQLRepositoryContext context)
        {
            _context = context;
        }

        public OwnedCoin? Create(OwnedCoin entity)
        {
            _context.OwnedCoins.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public OwnedCoin? Delete(long id)
        {
            var entity = _context.OwnedCoins.FirstOrDefault(o => o.Id == id);
            if (entity == null) return null;

            _context.OwnedCoins.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public OwnedCoin? Get(long id)
        {
            return _context.OwnedCoins.FirstOrDefault(o => o.Id == id);
        }

        public List<OwnedCoin> GetAll()
        {
            return _context.OwnedCoins.ToList();
        }

        public List<OwnedCoin> GetByCoin(long coinId)
        {
            return _context.OwnedCoins.Where(o => o.CoinId == coinId).ToList();
        }

        public OwnedCoin? Update(OwnedCoin entity)
        {
            var existing = _context.OwnedCoins.FirstOrDefault(o => o.Id == entity.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existing;
        }

        public List<OwnedCoin> UpdateByCoin(long coinId, List<OwnedCoin> coins)
        {
            var newCoins = coins.Where(c => c.Id == 0).ToList();
            foreach (var newOwned in newCoins)
            {
                newOwned.Id = DateTime.Now.Ticks;
            }
            _context.AddRange(newCoins);
            _context.SaveChanges();

            var banknoteIds = coins.Where(c => c.Id != 0).Select(c => c.Id).ToHashSet();
            var toRemove = _context.OwnedCoins.Where(e => !banknoteIds.Contains(e.Id)).ToList();

            _context.OwnedCoins.RemoveRange(toRemove);
            _context.SaveChanges();

            return coins;
        }
    }
}
