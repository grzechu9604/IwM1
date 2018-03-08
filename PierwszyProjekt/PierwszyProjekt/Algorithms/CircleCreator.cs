using System;
using System.Collections.Generic;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class CircleCreator
    {
        private int maxX;
        private int maxY;
        public List<Point> PointsOnCircle { private set; get; }

        public CircleCreator(int maxX, int maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.PointsOnCircle = CreateCircle();
        }

        private List<Point> CreateCircle()
        {
            List<Point> points = new List<Point>();

            int radius = maxX / 2;

            //rozwiązanie nieoptymalne
            for (int x = 0; x <= radius; x++)
            {
                Tuple<Point, double> min = new Tuple<Point, double>(new Point(x, 0), Math.Abs(Math.Pow(radius - x, 2) + Math.Pow(radius, 2) - Math.Pow(radius, 2)));

                for (int y = 0; y <= radius; y++)
                {
                    double currentValue = Math.Abs(Math.Pow(radius - x, 2) + Math.Pow(radius - y, 2) - Math.Pow(radius, 2));
                    if (min.Item2 >= currentValue)
                    {
                        min = new Tuple<Point, double>(new Point(x, y), currentValue);
                    }
                }
                points.Add(min.Item1);
            }

            //korzystanie z symetrii przed pętlą są elementy z jednej ćwiartki 
            // pomijamy pierwszy element zakładając że leżą idealnie po środku
            List<Point> _points = new List<Point>();
            _points.AddRange(points);
            Point previousPoint = new Point(-1,-1);
            _points.ForEach(p =>
            {
                if(previousPoint.X != -1)
                {
                    points.AddRange(new LineCreator(previousPoint, p).Line);
                }
                previousPoint = p;
            });

            _points.Clear();
            _points.AddRange(points);
            foreach (var point in _points)
            {
                // kolejne ćwiartki
                Point p = new Point(point.X, maxY - point.Y);
                points.Add(p);
                p = new Point(maxX - point.X, point.Y);
                points.Add(p);
                p = new Point(maxX - point.X, maxY - point.Y);
                points.Add(p);
            }

            return points;
        }
    }
}
