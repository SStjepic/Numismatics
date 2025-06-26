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
        public string SerialNumber { get; set; }
        public MoneyQuality Quality { get; set; }
        public long BanknoteId {  get; set; }

        public OwnedBanknoteDataViewModel() { }
        public OwnedBanknoteDataViewModel(long id, string serialNumber, MoneyQuality quality, long banknoteId)
        {
            Id = id;
            SerialNumber = serialNumber;
            Quality = quality;
            BanknoteId = banknoteId;
        }

        public OwnedBanknoteDataViewModel(string serialNumber, MoneyQuality quality, long banknoteId)
        {
            SerialNumber = serialNumber;
            Quality = quality;
            BanknoteId = banknoteId;
        }

        public OwnedBanknoteDataViewModel(OwnedBanknoteDTO ownedBanknote)
        {
            Id = ownedBanknote.Id;
            SerialNumber = ownedBanknote.SerialNumber;
            Quality = ownedBanknote.Quality;
        }

        public OwnedBanknoteDTO ToOwnedBanknoteDTO()
        {
            return new OwnedBanknoteDTO(Id,SerialNumber, Quality, BanknoteId);
        }

        public override bool Equals(object obj)
        {
            return obj is OwnedBanknoteDataViewModel model &&
                   Id == model.Id &&
                   SerialNumber == model.SerialNumber &&
                   Quality == model.Quality &&
                   BanknoteId == model.BanknoteId;
        }
    }
}
