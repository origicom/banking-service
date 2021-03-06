﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Types
{
    public struct MonthYear
    {
        public readonly Month Month;
        public readonly Year Year;

        public MonthYear(Month month, Year year)
        {
            Month = month;
            Year = year;
        }

        public override int GetHashCode()
        {
            return Year * 100 + (int)Month;
        }
    }

    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    [DebuggerDisplay("{_year}")]
    public struct Year
    {
        private readonly uint _year;

        public Year(uint year)
        {
            _year = year;
        }

        public static implicit operator uint(Year year)
        {
            return year._year;
        }

        public static explicit operator Year(uint year)
        {
            return new Year(year);
        }

        public static implicit operator int(Year year)
        {
            return (int)year._year;
        }
    }
}
