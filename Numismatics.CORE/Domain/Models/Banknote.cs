using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics
{
    public class Banknote
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public double Denomination { get; set; }
        public DateTime ReleaseDate {  get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture {  get; set; }
        public string Description { get; set; }
        public string City {  get; set; }
        public Banknote() { }

        public Banknote(int id, int countryId, int currencyId, double denomination, DateTime releaseDate, string obversePicture, string reversePicture, string description)
        {
            Id = id;
            CountryId = countryId;
            CurrencyId = currencyId;
            Denomination = denomination;
            ReleaseDate = releaseDate;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            Description = description;
        }
    }
}
