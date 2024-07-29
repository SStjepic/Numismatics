using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTO
{
    public class CoinDTO
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public Currency Currency { get; set; }
        public double Denomination { get; set; }
        public string Description { get; set; }
        public int NumberOfCoins { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public Dictionary<BanknoteQuality, int> Coins { get; set; }
        public CoinDTO(int id, Country country, Currency currency, double denomination, string description, int numberOfCoins, string obversePicture, string reversePicture)
        {
            Id = id;
            Country = country;
            Currency = currency;
            Denomination = denomination;
            Description = description;
            NumberOfCoins = numberOfCoins;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
        }


    }
}
