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
        public long? CountryId { get; set; }
        public long? CurrencyId { get; set; }
        public double? Value { get; set; }
        public Date? IssueDate {  get; set; }
        public string? Description { get; set; }
        public string? ObversePicture { get; set; }
        public string? ReversePicture { get; set; }
        public bool? IsSubunit {  get; set; }
        public string? City {  get; set; }

        public Coin() { }

        public Coin(long id, long? countryId, long? currencyId, double? value, Date? issueDate, string? description, string? obversePicture, string? reversePicture, bool? isSubunit, string? city)
        {
            Id = id;
            CountryId = countryId;
            CurrencyId = currencyId;
            Value = value;
            IssueDate = issueDate;
            Description = description;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            IsSubunit = isSubunit;
            City = city;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coin coin &&
                   Id == coin.Id;
        }
    }
}
