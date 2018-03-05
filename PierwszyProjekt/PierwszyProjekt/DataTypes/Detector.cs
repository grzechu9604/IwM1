using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
