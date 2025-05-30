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
        public long Id { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }
        public double Value { get; set; }
        public Date IssueDate { get; set; }
        public string Description { get; set; }
        public int NumberOfCoins { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public bool HundertPart {  get; set; }
        public Dictionary<MoneyQuality, int> Coins { get; set; }
        public CoinDTO() { }
        public CoinDTO(long id, CountryDTO country, CurrencyDTO currency, double value, string description, int numberOfCoins, string obversePicture, string reversePicture, Date issueDate, bool hundertPart, Dictionary<MoneyQuality, int> coins)
        {
            Id = id;
            Country = country;
            Currency = currency;
            Value = value;
            Description = description;
            NumberOfCoins = numberOfCoins;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            IssueDate = issueDate;
            HundertPart = hundertPart;
            Coins = coins;
        }

        public CoinDTO(Coin coin, Country country, Currency currency)
        {
            Id = coin.Id;
            Value = coin.Value;
            Description = coin.Description;
            NumberOfCoins = coin.NumberOfCoins;
            ObversePicture = coin.ObversePicture;
            ReversePicture = coin.ReversePicture;
            IssueDate = coin.IssueDate;
            Country = new CountryDTO(country);
            Currency = new CurrencyDTO(currency);
            HundertPart = coin.HundertPart;
            Coins = coin.Coins;
        }

        public Coin ToCoin()
        {
            return new Coin(Id, Country.Id, Currency.Id, Value, Description, NumberOfCoins, ObversePicture, ReversePicture, IssueDate, HundertPart, Coins);
        }

        public override bool Equals(object? obj)
        {
            return obj is CoinDTO dTO &&
                   Id == dTO.Id;
        }
    }
}
