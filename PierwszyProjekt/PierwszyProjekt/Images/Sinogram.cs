using PierwszyProjekt.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PierwszyProjekt.DataTypes;

namespace PierwszyProjekt.Images
{
    public class Sinogram : AbstractImage
    {
        private double[,] averageTable;
        public BaseImage OutPutImage { private set; get; }

        public long IterationAmount { set; get; }
        private double Alfa { set; get; }

        public Sinogram(BaseImage image, int n, double a, int l)
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

            DoRandonTransform(image, n, a, l);
        }



        private void DoRandonTransform(BaseImage image, int n, double a, int l)
        {
            CircleCreator cc = new CircleCreator(image.Bitmap.Width - 1, image.Bitmap.Height - 1);
            Circle circle = new Circle(image.Bitmap.Width - 1, image.Bitmap.Height - 1, image.Bitmap.Width / 2, cc.PointsOnCircle.ToArray());
            EmiterGenerator eg = new EmiterGenerator(a, circle);
            List<EmiterDetectorsSystem> systems = new List<EmiterDetectorsSystem>();

            BitmapToBlackAndWhiteConverter blackBitmap = new BitmapToBlackAndWhiteConverter(image.Bitmap);


            int suwak = Form1.trackbar * (eg.Emiters.Count / 10); 
            if(Form1.trackbar != 10) eg.Emiters.RemoveRange(suwak, (eg.Emiters.Count - suwak)-1);

            averageTable = new double[eg.Emiters.ToArray().Length, n + 1];
            double maxAverage = double.MinValue;
            double minAverage = double.MaxValue;

            int emiterIndex = 0;
            eg.Emiters.ForEach(e =>
            {
                DetectorsGenerator dg = new DetectorsGenerator(n, l, circle, e);
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
    }
}
