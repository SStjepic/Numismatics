using Numismatics.CORE.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Models
{
    public class OwnedCoin
    {
        public long Id { get; set; }
        public int NumberOfCoins { get; set; }
        public MoneyQuality Quality { get; set; }
        public long CoinId { get; set; }

        public OwnedCoin() { }

        public OwnedCoin(long id, int numberOfCoins, MoneyQuality quality, long coinId)
        {
            Id = id;
            NumberOfCoins = numberOfCoins;
            Quality = quality;
            CoinId = coinId;
        }
    }
}
