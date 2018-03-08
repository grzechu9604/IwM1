using PierwszyProjekt.DataTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PierwszyProjekt.Algorithms
{
    public class DetectorsGenerator
    {
        /// <summary>
        /// Use only for unit tests!!!
        /// </summary>
        public DetectorsGenerator() { }

        public DetectorsGenerator(int amountOfDetectors, int angularSpread, Circle circle, Emiter emiter)
        {
            Detectors = GenerateDetectors(amountOfDetectors, angularSpread, circle, emiter);
        }

        public DetectorsGenerator(int amountOfDetectors, List<Point> circle, Emiter emiter)
        {
            Detectors = GenerateDetectors(amountOfDetectors, circle, emiter);
        }

        public List<Detector> Detectors { private set; get; }

        private List<Detector> GenerateDetectors(int amountOfDetectors, int angularSpread, Circle circle, Emiter emiter)
        {
            List<Detector> detectors = new List<Detector>();

            double stepInDegrees = (double)angularSpread / amountOfDetectors;

            Point oppositePoint = OppositPoint(emiter.Point, circle.MaxX, circle.MaxY);

            for (double i = 0; i < angularSpread; i += stepInDegrees)
            {
                double distance = circle.Radius * 2 * Math.Sin(Math.PI * i / 180.0);
                //TODO na podstawie odległości od oppositPoint wyznaczyć odpowiedni punkt z orkegu
            }

            return detectors;
        }

        private List<Detector> GenerateDetectors(int amountOfDetectors, List<Point> circle, Emiter emiter)
        {
            List<Detector> detectors = new List<Detector>();

            int sizeOfCircle = circle.ToArray().Length;

            List<Point> possibleDetectorsPositions = circle.Where((p, i) => sizeOfCircle / 4 > i && i < 3 / 4 * sizeOfCircle).ToList();
            possibleDetectorsPositions.Where((p, i) => i % amountOfDetectors == 0);

            return detectors;
        }

        public Point OppositPoint(Point p, int maxX, int maxY)
        {
            return new Point(maxX - p.X, maxY - p.Y);
        }
    }
}
