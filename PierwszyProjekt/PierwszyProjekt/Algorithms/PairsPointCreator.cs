using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    /// to chyba będzie do wywalenia
    public class PairsPointCreator
    {
        public List<Tuple<Point, Point>> Pairs { private set; get; }

        public PairsPointCreator(double degree, List<Point> circle, int radius)
        {
            Pairs = GeneratePairs(degree, circle, radius);
        }

        private List<Tuple<Point, Point>> GeneratePairs(double degree, List<Point> circle, int radius)
        {
            List<Tuple<Point, Point>> pairs = new List<Tuple<Point, Point>>();

            circle.FindAll(p => p.X <= radius && p.Y <= radius).ForEach(p =>
            {
                pairs.Add(new Tuple<Point, Point>(p, GenerateAccordingPoint(degree, circle, radius, p)));
            });

            return pairs;
        }

        private Point GenerateAccordingPoint(double degree, List<Point> circle, int radius, Point point)
        {
            return new Point(0, 0);
        }

    }
}
