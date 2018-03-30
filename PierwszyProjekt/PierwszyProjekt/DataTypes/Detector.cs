using System.Drawing;

namespace PierwszyProjekt.DataTypes
{
    public class Detector
    {
        public Detector(Point point)
        {
            Point = point;
        }

        public Detector(int x, int y)
        {
            Point = new Point(x, y);
        }

        public Point Point { get; private set; }
    }
}
