using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.BanknoteViewModels
{
    public class OwnedBanknoteDataViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public MoneyQuality Quality { get; set; }
        public long BanknoteId {  get; set; }

        public OwnedBanknoteDataViewModel() { }
        public OwnedBanknoteDataViewModel(long id, string code, MoneyQuality quality, long banknoteId)
        {
            Id = id;
            Code = code;
            Quality = quality;
            BanknoteId = banknoteId;
        }

        public OwnedBanknoteDataViewModel(string code, MoneyQuality quality, long banknoteId)
        {
            Code = code;
            Quality = quality;
            BanknoteId = banknoteId;
        }

        public OwnedBanknoteDataViewModel(OwnedBanknoteDTO ownedBanknote)
        {
            Id = ownedBanknote.Id;
            Code = ownedBanknote.Code;
            Quality = ownedBanknote.Quality;
        }

        public OwnedBanknoteDTO ToOwnedBanknoteDTO()
        {
            return new OwnedBanknoteDTO(Id,Code, Quality, BanknoteId);
        }

        public override bool Equals(object obj)
        {
            return obj is OwnedBanknoteDataViewModel model &&
                   Id == model.Id &&
                   Code == model.Code &&
                   Quality == model.Quality &&
                   BanknoteId == model.BanknoteId;
        }
    }
}
