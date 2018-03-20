using System.Drawing;

namespace PierwszyProjekt.DataTypes
{
    public class Detector
    {
        public Detector(Point point)
        {
            Point = point;
        }

        public Point Point { get; private set; }
    }
}
