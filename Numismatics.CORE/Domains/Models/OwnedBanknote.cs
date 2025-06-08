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
        public string Code { get; set; }
        public MoneyQuality Quality { get; set; }
        public long BanknoteId { get; set; }

        public OwnedBanknote() { }

        public OwnedBanknote(long id, string code, MoneyQuality quality, long banknoteId)
        {
            Id = id;
            Code = code;
            Quality = quality;
            BanknoteId = banknoteId;
        }
    }
}
