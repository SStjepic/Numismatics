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
        public Dictionary<string, BanknoteQuality> Banknotes { get; set; }
        public BanknoteDTO(int id, Country country, Currency currency, double value, string obversePicture, string reversePicture, string description, Date issueDate)
        {
            Id = id;
            Country = country;
            Currency = currency;
            Value = value;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            Description = description;
            IssueDate = issueDate;
        }

    }
}
