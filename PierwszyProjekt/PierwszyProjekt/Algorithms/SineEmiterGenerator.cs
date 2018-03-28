using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PierwszyProjekt.DataTypes;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class SineEmiterGenerator : IEmiterGenerator
    {
        public SineEmiterGenerator(double alfa, int radius, int maxX, int maxY)
        {
            Emiters = GenerateEmiters(alfa, radius, maxX, maxY);
        }

        private List<Emiter> GenerateEmiters(double alfa, int radius, int maxX, int maxY)
        {
            List<Emiter> generatedEmiters = new List<Emiter>();

            Point centerOfCircle = new Point(maxX / 2, maxY / 2);

            for (double i = 0; i < 360; i+=alfa)
            {
                int x = Convert.ToInt32(centerOfCircle.X + radius * Math.Cos(DegreesToRadians(i)));
                int y = Convert.ToInt32(centerOfCircle.Y + radius * Math.Sin(DegreesToRadians(i)));
                generatedEmiters.Add(new Emiter(x, y));
            }

            return generatedEmiters;
        }

        private double DegreesToRadians(double angleInDegrees)
        {
            return Math.PI / 180 * angleInDegrees;
        }

        public List<Emiter> Emiters { private set; get; }

    }
}
