using Numismatics.CORE.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics.CORE.Domains.Models
{
    public class Coin
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long CurrencyId { get; set; }
        public double Value { get; set; }
        public Date IssueDate {  get; set; }
        public string Description { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public bool IsSubunit {  get; set; }

        public Coin() { }

        public Coin(long id, long countryId, long currencyId, double value, string description, string obversePicture, string reversePicture, Date issueDate, bool isSubunit)
        {
            Id = id;
            CountryId = countryId;
            CurrencyId = currencyId;
            Value = value;
            Description = description;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            IssueDate = issueDate;
            IsSubunit = isSubunit;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coin coin &&
                   Id == coin.Id;
        }
    }
}
