using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.DTOs;
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
    public class JsonBanknoteRepository : JSONRepository<Banknote>, IBanknoteRepository
    {
        private readonly string _fileName = "BanknoteData.json";

        public JsonBanknoteRepository(): base(new JSONSerialization())
        {
            SetFileName(_fileName);
        }

        public Banknote? Create(Banknote newBanknote)
        {
            var banknotes = GetAll();
            banknotes.Add(newBanknote);
            Save(banknotes);
            return newBanknote;
        }

        public Banknote? Delete(long banknoteId)
        {
            var oldBanknote = Get(banknoteId);
            if (oldBanknote == null) { return null; }
            var banknotes = GetAll();
            banknotes.Remove(oldBanknote);
            Save(banknotes);
            return oldBanknote;
        }

        public Banknote? Get(long id)
        {
            var banknotes = GetAll();
            var banknote = banknotes.FirstOrDefault(b => b.Id == id);
            return banknote;
        }

        public List<Banknote> GetAll()
        {
            return Load();
        }

        public Banknote? Update(Banknote newBanknote)
        {
            var banknotes = GetAll();
            var oldBanknote = Get(newBanknote.Id);
            if (oldBanknote != null)
            {
                banknotes.Remove(oldBanknote);
            }
            banknotes.Add(newBanknote);
            Save(banknotes);
            return newBanknote;
        }
        public int GetTotalBanknotesNumber()
        {
            return GetAll().Count();
        }

        public List<Banknote> GetByPage(int pageNumber, int pageSize, BanknoteSearchDataDTO searchParams)
        {
            var banknotes = this.GetAll().AsEnumerable();

            if (searchParams != null)
            {
                if (searchParams.Value > 0)
                {
                    banknotes = banknotes.Where(b => b.Value == searchParams.Value);
                }

                if (searchParams.Year > 0)
                {
                    banknotes = banknotes.Where(b => b.IssueDate.Year == searchParams.Year);
                }

                if (searchParams.Country?.Id > 0)
                {
                    banknotes = banknotes.Where(b => b.CountryId == searchParams.Country.Id);
                }

                if (searchParams.Currency?.Id > 0)
                {
                    banknotes = banknotes.Where(b => b.CurrencyId == searchParams.Currency.Id);
                }
            }

            banknotes = banknotes
                .OrderByDescending(b => b.IssueDate)
                .ThenBy(b => b.IsSubunit)
                .ThenByDescending(b => b.Value);

            return banknotes.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
    }
}
