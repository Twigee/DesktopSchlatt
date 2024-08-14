using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwigTools
{
    public static class Lerp
    {
        public static Point DoLerp(Point point1, Point point2, float t)
        {
            int x = (int)(point1.X + t * (point2.X - point1.X));
            int y = (int)(point1.Y + t * (point2.Y - point1.Y));
            return new Point(x, y);
        }

       
    }
}
