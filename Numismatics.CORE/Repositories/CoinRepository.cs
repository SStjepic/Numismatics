using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public class CoinRepository : Repository<Coin>, IRepository<Coin>
    {
        public CoinRepository() 
        {
            SetFileName("CoinData.json");
        }
        public Coin? Create(Coin newCoin)
        {
            var instances = GetAll();
            instances.Add(newCoin);
            Save(instances);
            return newCoin;
        }

        public Coin? Delete(int coinId)
        {
            var coins = GetAll();
            var oldCoin = Get(coinId);
            if(oldCoin == null) { return null; }
            coins.Remove(oldCoin);
            Save(coins);
            return oldCoin;
        }

        public Coin? Get(int id)
        {
            var coins = GetAll();
            return coins.Find(c => c.Id == id);
        }

        public List<Coin> GetAll()
        {
            return Load();
        }

        public Coin? Update(Coin newCoin)
        {
            var coins = GetAll();
            coins.RemoveAll(c => c.Id == newCoin.Id);
            coins.Add(newCoin);
            Save(coins);
            return newCoin;
        }
    }
}
