using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics
{
    public class Coin : MoneyItem
    {
        public int NumberOfCoins { get; set; }
        public Coin(int id, double denomination, string details, Country country, Currency currency, int numberOfCoins) 
            : base(id, denomination, details, country, currency)
        {
            NumberOfCoins = numberOfCoins;
        }


    }
}
