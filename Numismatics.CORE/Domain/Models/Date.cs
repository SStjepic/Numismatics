using Numismatics.CORE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domain.Models
{
    public class Date
    {
        public int Day {  get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Era Era { get; set; }
        public Date() { }
        public Date(int day, int month, int year, Era era)
        {
            Day = day;
            Month = month;
            Year = year;
            Era = era;
        }

        public Date(int year, Era era)
        {
            Year = year;
            Era = era;
        }




    }
}
