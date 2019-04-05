using System;
using System.Collections.Generic;
using System.Text;

namespace DnDSimulator
{
    public static class Extensions
    {
        public static bool IsBetween(this int value, int minimum, int maximum)
        {
            return (minimum <= value && value <= maximum);
        }
    }
}
