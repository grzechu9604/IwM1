using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PierwszyProjekt.Algorithms
{
    public class QuarterCircleCreator
    {
        private long maxX;
        private long maxY;
        public List<Point> PointsOnArc { private set; get; }

        public QuarterCircleCreator(long maxX, long maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.PointsOnArc = CreateQuarterOfCircle();
        }

        private List<Point> CreateQuarterOfCircle()
        {
            List<Point> points = new List<Point>();
            
            return points;
        }
    }
}
