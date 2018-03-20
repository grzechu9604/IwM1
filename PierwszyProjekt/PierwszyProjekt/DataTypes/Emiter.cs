using System.Drawing;

namespace PierwszyProjekt.DataTypes
{
    public class Emiter
    {
        public Emiter(Point point)
        {
            Point = point;
        }

        public Point Point { get; private set; }
    }
}
