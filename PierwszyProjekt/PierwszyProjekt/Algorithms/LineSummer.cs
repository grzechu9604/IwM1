using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class LineSummer
    {
        public double Sum { private set; get; }
        public double Average { private set; get; }
        public long AmountOfPoints { private set; get; }

        public LineSummer(IList<Point> points, double[,] tabToSum)
        {
            Calculate(points, tabToSum);
        }

        public void Calculate(IList<Point> points, double[,] tabToSum)
        {
            AmountOfPoints = 0;
            Average = 0;
            Sum = 0;

            double max = double.MinValue;
            double min = double.MaxValue;

            foreach (var point in points)
            {
                Sum += tabToSum[point.X, point.Y];

                if (max < tabToSum[point.X, point.Y])
                {
                    max = tabToSum[point.X, point.Y];
                }

                if (min > tabToSum[point.X, point.Y])
                {
                    min = tabToSum[point.X, point.Y];
                }

                AmountOfPoints++;
            }

            if (AmountOfPoints > 0)
            {
                if (AmountOfPoints > 5)
                {
                    Average = (Sum - max - min) / (AmountOfPoints - 2);
                }
                else
                {
                    Average = Sum / AmountOfPoints;
                }
            }
        }
    }
}
