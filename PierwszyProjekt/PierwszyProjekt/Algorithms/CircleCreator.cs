using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.Algorithms
{
    public class CircleCreator
    {
        private long maxX;
        private long maxY;
        public List<Point> PointsOnCircle { private set; get; }

        public CircleCreator(long maxX, long maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.PointsOnCircle = CreateCircle();
        }

        private List<Point> CreateCircle()
        {
            List<Point> points = new List<Point>();
            
            return points;
        }
    }
}
