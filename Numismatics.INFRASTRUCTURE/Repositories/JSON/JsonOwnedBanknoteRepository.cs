using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Repositories;
using Numismatics.INFRASTRUCTURE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.INFRASTRUCTURE.Repositories.JSON
{
    public class JsonOwnedBanknoteRepository :JSONRepository<OwnedBanknote>, IOwnedBanknotesRepository
    {
        private readonly string _fileName = "OwnedBanknoteData.json";

        public JsonOwnedBanknoteRepository(): base( new JSONSerialization()) 
        {
            SetFileName( _fileName );
        }
        public OwnedBanknote? Create(OwnedBanknote newOwnedBanknote)
        {
            var ownedBanknotes = GetAll();
            ownedBanknotes.Add(newOwnedBanknote);
            Save(ownedBanknotes);
            return newOwnedBanknote;
        }

        public OwnedBanknote? Delete(long id)
        {
            var oldOwnedBanknote = Get(id);
            if (oldOwnedBanknote == null) { return null; }
            var ownedBanknotes = GetAll();
            ownedBanknotes.Remove(oldOwnedBanknote);
            Save(ownedBanknotes);
            return oldOwnedBanknote;
        }

        public OwnedBanknote? Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<OwnedBanknote> GetAll()
        {
            return Load();
        }

        public OwnedBanknote? Update(OwnedBanknote ownedBanknote)
        {
            var allOwnedBanknotes = GetAll();
            var oldOwnedBanknote = Get(ownedBanknote.Id);
            if (oldOwnedBanknote != null)
            {
                allOwnedBanknotes.Remove(oldOwnedBanknote);
            }
            allOwnedBanknotes.Add(ownedBanknote);
            Save(allOwnedBanknotes);
            return ownedBanknote;
        }

        public List<OwnedBanknote> GetByBanknote(long banknoteId)
        {
            return Load().Where(o => o.BanknoteId == banknoteId).ToList();
        }

        public List<OwnedBanknote> UpdateByBanknote(long banknoteId, List<OwnedBanknote> banknotes)
        {
            List<OwnedBanknote> allOwnedBanknotes = GetAll();
            allOwnedBanknotes.RemoveAll(o => o.BanknoteId == banknoteId);
            allOwnedBanknotes.AddRange(banknotes);
            Save(allOwnedBanknotes);
            return banknotes;
        }
    }
}
