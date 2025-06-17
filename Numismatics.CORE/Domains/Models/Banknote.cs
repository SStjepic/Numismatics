using Numismatics.CORE.Domains.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Models
{
    public class Banknote
    {
        public long Id { get; set; }
        public long? CountryId { get; set; }
        public long? CurrencyId { get; set; }
        public double? Value { get; set; }
        public bool? IsSubunit { get; set; }
        public Date? IssueDate { get; set; }
        public string? ObversePicture { get; set; }
        public string? ReversePicture { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public Banknote() { }

        public Banknote(long id, long? countryId, long? currencyId, double? value, bool? isSubunit,
            Date? issueDate, string? obversePicture, string? reversePicture,
            string? description, string? city)
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
        }

        public override bool Equals(object? obj) => obj is Banknote b && Id == b.Id;
    }

}
