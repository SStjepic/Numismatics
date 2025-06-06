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
    }
}
