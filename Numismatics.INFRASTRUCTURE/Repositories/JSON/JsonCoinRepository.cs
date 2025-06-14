﻿using Numismatics.CORE.Domains.Models;
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
    public class JsonCoinRepository : JSONRepository<Coin>, ICoinRepository
    {
        public JsonCoinRepository(): base(new JSONSerialization())
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

        public Coin? Delete(long coinId)
        {
            var coins = GetAll();
            var oldCoin = Get(coinId);
            if (oldCoin == null) { return null; }
            coins.Remove(oldCoin);
            Save(coins);
            return oldCoin;
        }

        public Coin? Get(long id)
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

        public int GetTotalCoinsNumber()
        {
            return this.GetAll().Count();
        }

        public List<Coin> GetByPage(int pageNumber, int pageSize, CoinSearchDataDTO searchParams)
        {
            var coins = this.GetAll().AsEnumerable();

            if (searchParams != null)
            {
                if (searchParams.Value > 0)
                {
                    coins = coins
                        .Where(b => b.Value == searchParams.Value);
                }

                if (searchParams.Year > 0)
                {
                    coins = coins
                        .Where(b => b.IssueDate.Year == searchParams.Year);
                }

                if (searchParams.Country.Id > 0)
                {
                    coins = coins
                        .Where(b => b.CountryId == searchParams.Country.Id);
                }

                if (searchParams.Currency.Id > 0)
                {
                    coins = coins
                        .Where(b => b.CurrencyId == searchParams.Currency.Id);
                }
            }

            coins = coins
               .OrderByDescending(b => b.IssueDate)
               .ThenBy(b => b.IsSubunit)
               .ThenByDescending(b => b.Value);

            return coins.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
    }
}
