using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Precision
    {
        public static double Truncate2(this double number)
        {
            return Math.Truncate(number * 100) / 100;
        }
    }
}
