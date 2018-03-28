using PierwszyProjekt.DataTypes;
using System;
using System.Collections.Generic;

namespace PierwszyProjekt.Algorithms
{
    public class EmiterGenerator : IEmiterGenerator
    {
        public EmiterGenerator(double alfa, Circle circle)
        {
            Emiters = GenerateEmiters(360 / alfa, circle);
        }

        public List<Emiter> Emiters {  set; get; }

        private List<Emiter> GenerateEmiters(double amountOfEmiters, Circle circle)
        {
            int step = Convert.ToInt32(circle.AmountOfPoints / amountOfEmiters);
            if (step == 0)
            {
                step = 1;
            }

            List<Emiter> emiters = new List<Emiter>();

            for (int i = 0; i < circle.AmountOfPoints; i += step)
            {
                emiters.Add(new Emiter(circle.PointsOnACircle[i]));
            }

            return emiters;
        }
    }
}
