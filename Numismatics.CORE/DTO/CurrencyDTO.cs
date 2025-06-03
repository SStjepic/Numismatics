using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.DTO
{
    public class CurrencyDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SubunitName { get; set; }
        public string MainUnitName {  get; set; }
        public int SubunitToMainUnit { get; set; }
        public string Code { get; set; }
        public CurrencyDTO() { }
        public CurrencyDTO(Currency? currency)
        {
            if (currency != null) 
            {
                Id = currency.Id;
                Name = currency.Name;
                SubunitName = currency.SubunitName;
                MainUnitName = currency.MainUnitName;
                SubunitToMainUnit = currency.SubunitToMainUnit;
                Code = currency.Code;
            }
            else
            {
                Id = -1;
            }
        }
        public CurrencyDTO(long id, string name, string mainUnitName, string subunitName, int subunitToMainUnit, string code)
        {
            Id = id;
            Name = name;
            SubunitName = subunitName;
            MainUnitName = mainUnitName;
            SubunitToMainUnit = subunitToMainUnit;
            Code = code;
        }

        public Currency ToCurrency()
        {
            return new Currency(Id, Name,MainUnitName, SubunitName,SubunitToMainUnit,Code);
        }

        public override bool Equals(object? obj)
        {
            return obj is CurrencyDTO dTO &&
                   Id == dTO.Id;
        }
    }
}
