using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.DTOs;
using Numismatics.WPF.ViewModels.BanknoteViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.CoinViewModels
{
    public class OwnedCoinDataViewModel: INotifyPropertyChanged
    {
        public long Id { get; set; }

        private int _numberOfCoins;
        public int NumberOfCoins
        {
            get => _numberOfCoins;
            set
            {
                if (_numberOfCoins != value)
                {
                    _numberOfCoins = value;
                    OnPropertyChanged(nameof(NumberOfCoins));
                }
            }
        }
        public MoneyQuality Quality { get; set; }
        public long CoinId {  get; set; }

        public OwnedCoinDataViewModel() { }
        public OwnedCoinDataViewModel(long id, int numberOfCoins, MoneyQuality quality, long coinId)
        {
            Id = id;
            NumberOfCoins = numberOfCoins;
            Quality = quality;
            CoinId = coinId;
        }

        public OwnedCoinDataViewModel(OwnedCoinDTO ownedCoin)
        {
            Id = ownedCoin.Id;
            NumberOfCoins = ownedCoin.NumberOfCoins;
            Quality = ownedCoin.Quality;
        }

        public OwnedCoinDTO ToOwnedCoinDTO()
        {
            return new OwnedCoinDTO(Id, NumberOfCoins, Quality, CoinId);
        }

        public override bool Equals(object obj)
        {
            return obj is OwnedCoinDataViewModel model &&
                   Id == model.Id &&
                   NumberOfCoins == model.NumberOfCoins &&
                   Quality == model.Quality &&
                   CoinId == model.CoinId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
