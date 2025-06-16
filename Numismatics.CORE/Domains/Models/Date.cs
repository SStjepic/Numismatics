using Numismatics.CORE.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Models
{
    public class Date: IComparable<Date>
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


        public override string ToString()
        {
            var temp = "";
            temp += Year == 0? "*-" : Year.ToString() + "-";
            temp += Month == 0 ? "*-" : Month.ToString() + "-";
            temp += Day == 0 ? "*" : Day.ToString() ;

            return temp;
        }
        public static Date Parse(string value)
        {
            var parts = value.Split('-');
            int.TryParse(parts[0], out int year);
            int.TryParse(parts[1], out int month);
            int.TryParse(parts[2].TrimEnd('C', 'E', 'B'), out int day);

            Era era = value.Contains("BCE") ? Era.BCE : Era.CE;

            return new Date(day, month, year, era);
        }

        public int CompareTo(Date? other)
        {
            if (other == null) return 1;

            int eraComparison = Era.CompareTo(other.Era);
            if (eraComparison != 0)
                return eraComparison;

            int yearComparison = Year.CompareTo(other.Year);
            if (yearComparison != 0)
                return yearComparison;

            int monthComparison = Month.CompareTo(other.Month);
            if (monthComparison != 0)
                return monthComparison;

            return Day.CompareTo(other.Day);
        }

    }
}
