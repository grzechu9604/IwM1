using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            List<Point> keyPoints = GenerateKeyPointsInFirstQuarter();
            PointsOnCircle = GenerateWholeCirceFromArcInFirstHalf(keyPoints);
        }

        public List<Point> GenerateKeyPointsInFirstQuarter()
        {
            List<Point> points = new List<Point>();

            int radius = maxX / 2;
            
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

            return points;
        }
        
        public Point GeneratePointInSecoundQuarterOfCircle(Point pointInFirstHalf)
        {
            return new Point(maxX - pointInFirstHalf.X, pointInFirstHalf.Y);
        }

        public Point GeneratePointInThirdQuarterOfCircle(Point pointInFirstHalf)
        {
            return new Point(maxX - pointInFirstHalf.X, maxY - pointInFirstHalf.Y);
        }

        public Point GeneratePointInForthQuarterOfCircle(Point pointInFirstHalf)
        {
            return new Point(pointInFirstHalf.X, maxY - pointInFirstHalf.Y);
        }

        public List<Point> GenerateWholeCirceFromArcInFirstHalf(List<Point> arc)
        {
            List<Point> secoudQuarterArc = new List<Point>();
            List<Point> thirdQuarterArc = new List<Point>();
            List<Point> forthQuarterArc = new List<Point>();

            arc.ForEach(p => 
            {
                secoudQuarterArc.Add(GeneratePointInSecoundQuarterOfCircle(p));
                thirdQuarterArc.Add(GeneratePointInThirdQuarterOfCircle(p));
                forthQuarterArc.Add(GeneratePointInForthQuarterOfCircle(p));
            });

            List<Point> circle = arc;
            secoudQuarterArc.Reverse();
            forthQuarterArc.Reverse();
            circle.AddRange(secoudQuarterArc);
            circle.AddRange(thirdQuarterArc);
            circle.AddRange(forthQuarterArc);

            return circle;
        }
    }
}
