using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTO
{
    public class BanknoteDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
        public CurrencyDTO Currency { get; set; }
        public double Value { get; set; }
        public bool HundertPart { get; set; }

        public Date IssueDate { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public Dictionary<string, MoneyQuality> Banknotes { get; set; }
        public BanknoteDTO(int id, CountryDTO country, CurrencyDTO currency, double value, bool hundertPart, string obversePicture, string reversePicture, string description, Date issueDate, string city, Dictionary<string, MoneyQuality> banknotes)
        {
            Id = id;
            Country = country;
            Currency = currency;
            Value = value;
            HundertPart = hundertPart;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            Description = description;
            IssueDate = issueDate;
            City = city;
            Banknotes = banknotes;
        }

        public BanknoteDTO(Banknote banknote, Country country, Currency currency) 
        {
            Id = banknote.Id;
            Country = new CountryDTO(country);
            Currency = new CurrencyDTO(currency);
            Value = banknote.Value;
            IssueDate = banknote.IssueDate;
            ObversePicture = banknote.ObversePicture;
            ReversePicture = banknote.ReversePicture;
            Description = banknote.Description;
            City = banknote.City;
            Banknotes = banknote.Banknotes;
        }

        public override bool Equals(object? obj)
        {
            return obj is BanknoteDTO dTO &&
                   Id == dTO.Id;
        }

        public Banknote ToBanknote()
        {
            return new Banknote(Id, Country.Id, Currency.Id, Value,HundertPart, IssueDate, ObversePicture, ReversePicture, Description, City, Banknotes);
        }
    }
}
