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
    public class OwnedBanknote
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; }
        public MoneyQuality Quality { get; set; }
        public long BanknoteId { get; set; }

        public OwnedBanknote() { }

        public OwnedBanknote(long id, string serialNumber, MoneyQuality quality, long banknoteId)
        {
            Id = id;
            SerialNumber = serialNumber;
            Quality = quality;
            BanknoteId = banknoteId;
        }

        public override bool Equals(object? obj)
        {
            return obj is OwnedBanknote banknote &&
                   Id == banknote.Id;
        }
    }
}
