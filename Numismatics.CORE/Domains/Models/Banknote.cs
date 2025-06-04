using Numismatics.CORE.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Models
{
    public class Banknote
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long CurrencyId { get; set; }
        public double Value { get; set; }
        public bool IsSubunit { get; set; }
        public Date IssueDate { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public Dictionary<string, MoneyQuality> Banknotes { get; set; }
        public Banknote() { }

        public Banknote(long id, long countryId, long currencyId, double value, bool isSubunit, Date issueDate, string obversePicture, string reversePicture, string description, string city, Dictionary<string, MoneyQuality> banknotes)
        {
            Id = id;
            CountryId = countryId;
            CurrencyId = currencyId;
            Value = value;
            IsSubunit = isSubunit;
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
