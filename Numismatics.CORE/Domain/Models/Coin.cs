using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Numismatics
{
    public class Coin 
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public double Denomination { get; set; }
        public string Description { get; set; }
        public int NumberOfCoins { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }

        public Coin() { }

        public Coin(int id, int countryId, int currencyId, double denomination, string description, int numberOfCoins, string obversePicture, string reversePicture)
        {
            Id = id;
            CountryId = countryId;
            CurrencyId = currencyId;
            Denomination = denomination;
            Description = description;
            NumberOfCoins = numberOfCoins;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
        }
    }
}
