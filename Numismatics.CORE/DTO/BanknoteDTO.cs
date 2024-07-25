using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.DTO
{
    public class BanknoteDTO
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public Currency Currency { get; set; }
        public double Denomination { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ObversePicture { get; set; }
        public string ReversePicture { get; set; }
        public string Description { get; set; }
        public Dictionary<string, BanknoteQuality> Banknotes {  get; set; }
        public BanknoteDTO(int id, Country country, Currency currency, double denomination, DateTime releaseDate, string obversePicture, string reversePicture, string description)
        {
            Id = id;
            Country = country;
            Currency = currency;
            Denomination = denomination;
            ReleaseDate = releaseDate;
            ObversePicture = obversePicture;
            ReversePicture = reversePicture;
            Description = description;
        }

    }
}
