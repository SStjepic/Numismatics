using Numismatics.CORE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.CORE.Domain.Models
{
    public class Coin
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long CurrencyId { get; set; }
        public double Value { get; set; }
        public Date IssueDate {  get; set; }
        public string Description { get; set; }
        public int NumberOfCoins { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public bool HundertPart {  get; set; }
        public Dictionary<MoneyQuality, int> Coins;

        public Coin() { }

        public Coin(long id, long countryId, long currencyId, double value, string description, int numberOfCoins, string obversePicture, string reversePicture, Date issueDate, bool hundertPart, Dictionary<MoneyQuality, int> coins)
        {
            Id = id;
            CountryId = countryId;
            CurrencyId = currencyId;
            Value = value;
            Description = description;
            NumberOfCoins = numberOfCoins;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            IssueDate = issueDate;
            HundertPart = hundertPart;
            Coins = coins;  
        }

        public override bool Equals(object? obj)
        {
            return obj is Coin coin &&
                   Id == coin.Id;
        }
    }
}
