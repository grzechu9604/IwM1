using System.Collections.Generic;
using System.Drawing;

namespace PierwszyProjekt.DataTypes
{
    public class Circle
    {
        public Point[] PointsOnACircle { private set; get; }
        public Point CenterOfTheCircle { private set; get; }
        public int Radius { private set; get; }
        public int AmountOfPoints { private set; get; }
        public int MaxX { private set; get; }
        public int MaxY { private set; get; }

        public Point GetPointAtNthPositionUsingModulo(int position)
        {
            return PointsOnACircle[position % AmountOfPoints];
        }

        public int GetIndex(Point p)
        {
            for (int i = 0; i < PointsOnACircle.Length; i++)
            {
                if (p.X == PointsOnACircle[i].X && p.Y == PointsOnACircle[i].Y)
                    return i;
            }
            throw new KeyNotFoundException();
        }

        public Circle(int maxX, int maxY, int radius, Point[] pointsOnACircle)
        {
            CenterOfTheCircle = new Point(maxX / 2, maxY / 2);
            Radius = radius;
            PointsOnACircle = pointsOnACircle;
            AmountOfPoints = PointsOnACircle.Length;
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
