using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TwigTools
{
    public static class TwigMath
    {
        public static double Distance(System.Drawing.Point a, System.Drawing.Point b)
        {
            var result = Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
            return result;

        }
    }
}
