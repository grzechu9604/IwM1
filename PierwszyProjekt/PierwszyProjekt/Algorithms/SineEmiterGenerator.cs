using PierwszyProjekt.DataTypes;
using System.Collections.Generic;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class SineEmiterGenerator : IEmiterGenerator
    {
        public SineEmiterGenerator(double alfa, int radius, int maxX, int maxY, Point centerOfCircle)
        {
            Emiters = GenerateEmiters(alfa, radius, maxX, maxY, centerOfCircle);
        }

        private List<Emiter> GenerateEmiters(double alfa, int radius, int maxX, int maxY, Point centerOfCircle)
        {
            List<Emiter> generatedEmiters = new List<Emiter>();
         
            for (double i = 0; i < 360; i+=alfa)
            {
                double angleInRadians = CircleUtil.DegreesToRadians(i);
                Point newEmiterPoint = CircleUtil.GeneratePointOnArc(centerOfCircle, radius, angleInRadians);
                generatedEmiters.Add(new Emiter(newEmiterPoint, angleInRadians));
            }

            return generatedEmiters;
        }

        public List<Emiter> Emiters { private set; get; }

    }
}
