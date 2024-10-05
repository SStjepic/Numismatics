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
        public Country Country { get; set; }
        public Currency Currency { get; set; }
        public double Value { get; set; }
        public Date IssueDate { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public Dictionary<string, BanknoteQuality> Banknotes { get; set; }
        public BanknoteDTO(int id, Country country, Currency currency, double value, string obversePicture, string reversePicture, string description, Date issueDate, string city, Dictionary<string, BanknoteQuality> banknotes)
        {
            Id = id;
            Country = country;
            Currency = currency;
            Value = value;
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
            Country = country;
            Currency = currency;
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
    }
}
