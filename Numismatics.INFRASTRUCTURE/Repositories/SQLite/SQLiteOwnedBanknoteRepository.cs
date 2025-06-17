using Microsoft.EntityFrameworkCore;
using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Numismatics.INFRASTRUCTURE.Repositories.SQLite
{
    public class SQLiteOwnedBanknoteRepository : IOwnedBanknotesRepository
    {
        private readonly SQLRepositoryContext _context;

        public SQLiteOwnedBanknoteRepository(SQLRepositoryContext context)
        {
            _context = context;
        }

        public OwnedBanknote? Create(OwnedBanknote entity)
        {
            _context.OwnedBanknotes.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public OwnedBanknote? Delete(long id)
        {
            var entity = _context.OwnedBanknotes.FirstOrDefault(o => o.Id == id);
            if (entity == null) return null;

            _context.OwnedBanknotes.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public OwnedBanknote? Get(long id)
        {
            return _context.OwnedBanknotes.FirstOrDefault(o => o.Id == id);
        }

        public List<OwnedBanknote> GetAll()
        {
            return _context.OwnedBanknotes.ToList();
        }

        public List<OwnedBanknote> GetByBanknote(long banknoteId)
        {
            return _context.OwnedBanknotes
                .Where(o => o.BanknoteId == banknoteId)
                .ToList();
        }

        public OwnedBanknote? Update(OwnedBanknote entity)
        {
            var existing = _context.OwnedBanknotes.FirstOrDefault(o => o.Id == entity.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return existing;
        }

        public List<OwnedBanknote> UpdateByBanknote(long banknoteId, List<OwnedBanknote> banknotes)
        {
            var existing = _context.OwnedBanknotes.Where(o => o.BanknoteId == banknoteId).ToList();
            _context.OwnedBanknotes.RemoveRange(existing);

            foreach (var banknote in banknotes)
            {
                _context.OwnedBanknotes.Add(banknote);
            }

            _context.SaveChanges();
            return banknotes;
        }
    }
}
