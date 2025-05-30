using Numismatics.CORE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domain.Models
{
    public class Banknote
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long CurrencyId { get; set; }
        public double Value { get; set; }
        public bool HundertPart { get; set; }
        public Date IssueDate { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public Dictionary<string, MoneyQuality> Banknotes { get; set; }
        public Banknote() { }

        public Banknote(long id, long countryId, long currencyId, double value, bool hundertPart, Date issueDate, string obversePicture, string reversePicture, string description, string city, Dictionary<string, MoneyQuality> banknotes)
        {
            Id = id;
            CountryId = countryId;
            CurrencyId = currencyId;
            Value = value;
            HundertPart = hundertPart;
            IssueDate = issueDate;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            Description = description;
            City = city;
            Banknotes = banknotes;
        }

        public override bool Equals(object? obj)
        {
            return obj is Banknote banknote &&
                   Id == banknote.Id;
        }
    }
}
