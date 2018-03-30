using PierwszyProjekt.Algorithms;
using PierwszyProjekt.DataTypes;
using System;
using System.Collections.Generic;

namespace PierwszyProjekt.Images
{
    public class Sinogram : AbstractImage
    {
        private double[,] averageTable;
        public BaseImage OutPutImage { private set; get; }

        public long IterationAmount { set; get; }
        private double Alfa { set; get; }

        public Sinogram(BaseImage image, int n, double a, int l, bool useKeyPoints)
        {
            OutPutImage = new BaseImage(image.Bitmap.Width, image.Bitmap.Height);
            OutPutImage.SumOfAverageTable = new double[image.Bitmap.Width, image.Bitmap.Height];
            OutPutImage.CountOfAverageTable = new double[image.Bitmap.Width, image.Bitmap.Height];

            for (int i = 0; i < image.Bitmap.Width; i++)
            {
                for (int j = 0; j < image.Bitmap.Height; j++)
                {
                    OutPutImage.SumOfAverageTable[i, j] = 0;
                    OutPutImage.CountOfAverageTable[i, j] = 0;
                }
            }

            DoRandonTransform(image, n, a, l, useKeyPoints);
        }



        private void DoRandonTransform(BaseImage image, int n, double a, int l, bool useKeyPoints)
        {
            int maxX = image.Bitmap.Width - 1;
            int maxY = image.Bitmap.Height - 1;
            int radius = image.Bitmap.Width / 2 - 1;

            CircleCreator cc = new CircleCreator(maxX, maxY);
            Circle circle = new Circle(maxX, maxY, radius, cc.PointsOnCircle.ToArray());

            IEmiterGenerator eg = CreateEmiterGenerator(useKeyPoints, a, circle, maxX, maxY, radius);
               
            List<EmiterDetectorsSystem> systems = new List<EmiterDetectorsSystem>();

            BitmapToBlackAndWhiteConverter blackBitmap = new BitmapToBlackAndWhiteConverter(image.Bitmap);


            int suwak = Form1.trackbar * (eg.Emiters.Count / 10);
            if (Form1.trackbar != 10) eg.Emiters.RemoveRange(suwak, (eg.Emiters.Count - suwak) - 1);

            averageTable = new double[eg.Emiters.ToArray().Length, n + 1];
            double maxAverage = double.MinValue;
            double minAverage = double.MaxValue;

            int emiterIndex = 0;
            eg.Emiters.ForEach(e =>
            {
                IDetectorsGenerator dg = CreateDetectorGenerator(useKeyPoints, n, l, circle, e, radius);
                EmiterDetectorsSystem newSystem = new EmiterDetectorsSystem(e, dg.Detectors);
                systems.Add(newSystem);

                int detectorIndex = 0;
                newSystem.Detectors.ForEach(detector =>
                    {
                        LineCreator lc = new LineCreator(e.Point, detector.Point);
                        LineSummer summer = new LineSummer(lc.Line, blackBitmap.ConvertedTab);

                        lc.Line.ForEach(pointOnLine =>
                        {
                            OutPutImage.SumOfAverageTable[pointOnLine.X, pointOnLine.Y] += summer.Average;
                            OutPutImage.CountOfAverageTable[pointOnLine.X, pointOnLine.Y] += 1;
                        });

                        averageTable[emiterIndex, detectorIndex++] = summer.Average;

                        if (summer.Average > maxAverage)
                        {
                            maxAverage = summer.Average;
                        }

                        if (summer.Average < minAverage)
                        {
                            minAverage = summer.Average;
                        }
                    });
                emiterIndex++;
            });


            NormalizeTab(eg.Emiters.ToArray().Length, n + 1, maxAverage, averageTable, minAverage);
            GenerateBitmap(eg.Emiters.ToArray().Length, n + 1, averageTable);

            Console.Write("DoRandonTransform --> DONE\n");
        }

        private IEmiterGenerator CreateEmiterGenerator(bool useKeyPoints, double alfa, Circle circle, int maxX, int maxY, int radius)
        {
            IEmiterGenerator eg;
            if (useKeyPoints)
            {
                eg = new EmiterGenerator(alfa, circle);
            }
            else
            {
                eg = new SineEmiterGenerator(alfa, radius, maxX, maxY, circle.CenterOfTheCircle);
            }

            return eg;
        }

        private IDetectorsGenerator CreateDetectorGenerator(bool useKeyPoints, int amountOfDetectors, double angularSpread, Circle circle, Emiter emiter, int radius)
        {
            IDetectorsGenerator dg;

            if (useKeyPoints)
            {
                dg = new DetectorsGenerator(amountOfDetectors, angularSpread, circle, emiter);
            }
            else
            {
                dg = new SineDetectorsGenerator(amountOfDetectors, angularSpread, emiter, radius, circle.CenterOfTheCircle);
            }

            return dg;
        }
    }
}
