using Numismatics.CORE.DTO;
using Numismatics.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services
{
    public class NationalCurrencyService
    {
        private NationalCurrencyRepository _repository;
        
        public NationalCurrencyService() 
        {
            _repository = new NationalCurrencyRepository();
        }

        public NationalCurrencyDTO? Create(NationalCurrencyDTO? nationalCurrencyDTO)
        {
            nationalCurrencyDTO.Id = HashCode.Combine(nationalCurrencyDTO.Currency.ToCurrency());
            _repository.Create(nationalCurrencyDTO.ToNationalCurrency());
            return nationalCurrencyDTO;
        }

        public List<int> GetCountriesByCurrency(int id)
        {
            var countryIds = new List<int>();

            foreach (var nationalCurrency in _repository.GetAll())
            {
                if (nationalCurrency.CurrencyId == id)
                {
                    foreach (var country in nationalCurrency.Countries)
                    {
                        countryIds.Add(country);
                    }
                }
            }

            return countryIds;
        }
    }
}
