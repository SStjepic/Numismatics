using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Repositories;
using Numismatics.CORE.Serialization.Interface;
using Numismatics.INFRASTRUCTURE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.INFRASTRUCTURE.Repositories.JSON
{
    public class JsonOwnedCoinRepository : JSONRepository<OwnedCoin>, IOwnedCoinRepository
    {
        private readonly string _fileName = "OwnedCoinData.json";
        public JsonOwnedCoinRepository() : base(new JSONSerialization())
        {
            SetFileName(_fileName);
        }

        public OwnedCoin? Create(OwnedCoin newOwnedCoin)
        {
            var ownedCoins = GetAll();
            ownedCoins.Add(newOwnedCoin);
            Save(ownedCoins);
            return newOwnedCoin;
        }

        public OwnedCoin? Delete(long id)
        {
            var oldOwnedCoin = Get(id);
            if (oldOwnedCoin == null) { return null; }
            var ownedCoins = GetAll();
            ownedCoins.Remove(oldOwnedCoin);
            Save(ownedCoins);
            return oldOwnedCoin;
        }

        public OwnedCoin? Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<OwnedCoin> GetAll()
        {
            return Load();
        }
        public OwnedCoin? Update(OwnedCoin ownedCoin)
        {
            var allOwnedCoins = GetAll();
            var oldOwnedCoin = Get(ownedCoin.Id);
            if (oldOwnedCoin != null)
            {
                allOwnedCoins.Remove(oldOwnedCoin);
            }
            allOwnedCoins.Add(ownedCoin);
            Save(allOwnedCoins);
            return ownedCoin;
        }
        public List<OwnedCoin> GetByCoin(long coinId)
        {
            return Load().Where(o => o.CoinId == coinId).ToList(); 
        }


        public List<OwnedCoin> UpdateByCoin(long coinId, List<OwnedCoin> coins)
        {
            List<OwnedCoin> allOwnedCoins = GetAll();
            allOwnedCoins.RemoveAll(o => o.CoinId == coinId);
            allOwnedCoins.AddRange(coins);
            Save(allOwnedCoins);
            return coins;
        }
    }
}
