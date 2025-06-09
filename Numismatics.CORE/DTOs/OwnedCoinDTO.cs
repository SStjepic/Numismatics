using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class OwnedCoinDTO
    {
        public long Id { get; set; }
        public int NumberOfCoins{ get; set; }
        public MoneyQuality Quality { get; set; }
        public long CoinId { get; set; }

        public OwnedCoinDTO() { }

        public OwnedCoinDTO(OwnedCoin ownedCoin)
        {
            Id = ownedCoin.Id;
            NumberOfCoins = ownedCoin.NumberOfCoins;
            Quality = ownedCoin.Quality;
            CoinId = ownedCoin.CoinId;
        }
        public OwnedCoinDTO(long id, int numberOfCoins, MoneyQuality quality, long coinId)
        {
            Id = id;
            NumberOfCoins = numberOfCoins;
            Quality = quality;
            CoinId = coinId;
        }

        public OwnedCoin ToOwnedCoin()
        {
            return new OwnedCoin(Id, NumberOfCoins, Quality, CoinId);
        }
    }
}
