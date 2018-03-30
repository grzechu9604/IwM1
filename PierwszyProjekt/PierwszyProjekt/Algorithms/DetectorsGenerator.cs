using PierwszyProjekt.DataTypes;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class DetectorsGenerator : IDetectorsGenerator
    {
        /// <summary>
        /// Use only for unit tests!!!
        /// </summary>
        public DetectorsGenerator() { }

        public DetectorsGenerator(int amountOfDetectors, double angularSpread, Circle circle, Emiter emiter)
        {
            Detectors = GenerateDetectors(amountOfDetectors, angularSpread, circle, emiter);
        }
        
        public List<Detector> Detectors { private set; get; }

        private List<Detector> GenerateDetectors(int amountOfDetectors, double angularSpread, Circle circle, Emiter emiter)
        {
            List<Detector> detectors = new List<Detector>();

            int amountOfPossibleDetectorsPoints = Convert.ToInt32(angularSpread * circle.AmountOfPoints / 360);
            int stepInPoints = amountOfPossibleDetectorsPoints / amountOfDetectors;

            if (stepInPoints == 0)
            {
                stepInPoints = 1;
            }

            int skipPointsAtTheBegginning = (circle.AmountOfPoints - amountOfPossibleDetectorsPoints) / 2;
            int emiterIndex = circle.GetIndex(emiter.Point);
            int createdDetectors = 0;

            int index = emiterIndex + skipPointsAtTheBegginning;
            while (createdDetectors != amountOfDetectors)
            {
                detectors.Add(new Detector(circle.GetPointAtNthPositionUsingModulo(index)));
                createdDetectors++;
                index += stepInPoints;
            }

            return detectors;
        }

        public Point OppositPoint(Point p, int maxX, int maxY)
        {
            return new Point(maxX - p.X, maxY - p.Y);
        }
    }
}
