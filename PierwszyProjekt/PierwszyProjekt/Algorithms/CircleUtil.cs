using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public static class CircleUtil
    {
        public static double DegreesToRadians(double angleInDegrees)
        {
            return Math.PI / 180 * angleInDegrees;
        }

        public static Point GeneratePointOnArc(Point centerOfTheCircle, int radius, double angle)
        {
            int x = Convert.ToInt32(centerOfTheCircle.X + radius * Math.Cos(angle));
            int y = Convert.ToInt32(centerOfTheCircle.Y + radius * Math.Sin(angle));
            return new Point(x, y);
        }
    }
}
