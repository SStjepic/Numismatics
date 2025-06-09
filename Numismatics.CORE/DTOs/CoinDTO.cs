using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class CoinDTO
    {
        public long Id { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }
        public double Value { get; set; }
        public Date IssueDate { get; set; }
        public string Description { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public bool IsSubunit {  get; set; }
        public List<OwnedCoinDTO> OwnedCoins { get; set; }
        public CoinDTO() { }

        public CoinDTO(long id, CountryDTO country, CurrencyDTO currency, double value, Date issueDate, string description, string obversePicture, string reversePicture, bool isSubunit, List<OwnedCoinDTO> ownedCoins)
        {
            Id = id;
            Country = country;
            Currency = currency;
            Value = value;
            IssueDate = issueDate;
            Description = description;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            IsSubunit = isSubunit;
            OwnedCoins = ownedCoins;
        }

        public CoinDTO(Coin coin, Country country, Currency currency, List<OwnedCoinDTO> ownedCoins)
        {
            Id = coin.Id;
            Value = coin.Value;
            Description = coin.Description;
            ObversePicture = coin.ObversePicture;
            ReversePicture = coin.ReversePicture;
            IssueDate = coin.IssueDate;
            Country = new CountryDTO(country);
            Currency = new CurrencyDTO(currency);
            IsSubunit = coin.IsSubunit;
            OwnedCoins = ownedCoins;
        }

        public Coin ToCoin()
        {
            long countryId = Country != null ? Country.Id : -1;
            long currencyId = Currency != null ? Currency.Id : -1;
            return new Coin(Id, countryId, currencyId, Value, Description, ObversePicture, ReversePicture, IssueDate, IsSubunit);
        }

        public override bool Equals(object? obj)
        {
            return obj is CoinDTO dTO &&
                   Id == dTO.Id;
        }
    }
}
