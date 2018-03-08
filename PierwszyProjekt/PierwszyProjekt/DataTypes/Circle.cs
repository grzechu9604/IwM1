using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.DataTypes
{
    public class Circle
    {
        public Point[] PointsOnACircle { private set; get; }
        public int Radius { private set; get; }
        public int AmountOfPoints { private set; get; }
        public int MaxX { private set; get; }
        public int MaxY { private set; get; }


        public Circle(int maxX, int maxY, int radius, Point[] pointsOnACircle)
        {
            Radius = radius;
            PointsOnACircle = pointsOnACircle;
            AmountOfPoints = PointsOnACircle.Length;
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
