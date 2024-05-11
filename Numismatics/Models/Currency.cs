using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string hunderthPartName { get; set; }
        public string Code {  get; set; }
        public int Number {  get; set; }

        public Currency(int id, string name, string hunderthPartName, string code, int number)
        {
            Id = id;
            Name = name;
            this.hunderthPartName = hunderthPartName;
            Code = code;
            Number = number;
        }
    }
}
