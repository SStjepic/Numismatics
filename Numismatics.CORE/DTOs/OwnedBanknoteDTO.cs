using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTOs
{
    public class OwnedBanknoteDTO
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; }
        public MoneyQuality Quality { get; set; }
        public long BanknoteId { get; set; }

        public OwnedBanknoteDTO() { }

        public OwnedBanknoteDTO(long id, string serialNumber, MoneyQuality quality, long banknoteId)
        {
            Id = id;
            SerialNumber = serialNumber;
            Quality = quality;
            this.BanknoteId = banknoteId;
        }

        public OwnedBanknoteDTO(OwnedBanknote ownedBanknote)
        {
            Id = ownedBanknote.Id;
            SerialNumber = ownedBanknote.SerialNumber;
            Quality = ownedBanknote.Quality;
            BanknoteId = ownedBanknote.BanknoteId;
        }

        public OwnedBanknote ToOwnedBanknote()
        {
            return new OwnedBanknote(Id, SerialNumber, Quality, BanknoteId);
        }
    }
}
