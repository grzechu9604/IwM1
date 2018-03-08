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
            CreateCircle();
        }

        public void CreateCircle()
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
            
            List<Point> secoundQuarter = new List<Point>();
            List<Point> thirdQuarter = new List<Point>();
            List<Point> forthQuarter = new List<Point>();

            foreach (var point in _points)
            {
                // kolejne ćwiartki
                Point p = new Point(point.X, maxY - point.Y);
                forthQuarter.Add(p);
                p = new Point(maxX - point.X, point.Y);
                secoundQuarter.Add(p);
                p = new Point(maxX - point.X, maxY - point.Y);
                thirdQuarter.Add(p);
            }

            #region debug


            int[,] tab = new int[maxX + 1, maxY + 1];
            points.ForEach(p => tab[p.X, p.Y] = 1);
            secoundQuarter.ForEach(p => tab[p.X, p.Y] = 2);
            thirdQuarter.ForEach(p => tab[p.X, p.Y] = 3);
            forthQuarter.ForEach(p => tab[p.X, p.Y] = 4);

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    Console.Write(tab[i, j]);
                }
                Console.WriteLine();
            }

            #endregion

            PointsOnCircle = points;
        }
    }
}
