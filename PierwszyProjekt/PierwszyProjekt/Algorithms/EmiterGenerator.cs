using PierwszyProjekt.DataTypes;
using System.Collections.Generic;

namespace PierwszyProjekt.Algorithms
{
    public class EmiterGenerator
    {
        public EmiterGenerator(int alfa, Circle circle)
        {
            Emiters = GenerateEmiters(360 / alfa, circle);
        }

        public List<Emiter> Emiters { private set; get; }
        
        private List<Emiter> GenerateEmiters(int amountOfEmiters, Circle circle)
        {
            int amountOfPointsInFirstHalf = circle.AmountOfPoints / 2;
            int step = amountOfPointsInFirstHalf / amountOfEmiters;
            List<Emiter> emiters = new List<Emiter>();

            for (int i = 0; i < amountOfPointsInFirstHalf; i += step)
            {
                emiters.Add(new Emiter(circle.PointsOnACircle[i]));
            }

            return emiters;
        }
    }
}
