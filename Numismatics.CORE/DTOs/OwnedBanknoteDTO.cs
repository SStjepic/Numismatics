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
        public string Code { get; set; }
        public MoneyQuality Quality { get; set; }
        public long banknoteId;

        public OwnedBanknoteDTO() { }

        public OwnedBanknoteDTO(string key, MoneyQuality quality, long banknoteId)
        {
            Code = key;
            Quality = quality;
            this.banknoteId = banknoteId;
        }

        public OwnedBanknoteDTO(OwnedBanknote ownedBanknote)
        {
            Id = ownedBanknote.Id;
            Code = ownedBanknote.Code;
            Quality = ownedBanknote.Quality;
            banknoteId = ownedBanknote.BanknoteId;
        }

        public OwnedBanknote ToOwnedBanknote()
        {
            return new OwnedBanknote(Id, Code, Quality, banknoteId);
        }
    }
}
