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
    public class Sinogram : IDisplayable
    {
        public Bitmap Bitmap { private set; get; }
        private double[,] averageTable;
        public BaseImage outPutImage { private set; get; }

        public long IterationAmount { set; get; }
        private double Alfa { set; get; }

        public Sinogram(BaseImage image, int n, int a, int l)
        {
            outPutImage = new BaseImage(image.Bitmap.Width, image.Bitmap.Height);
            outPutImage.sumOfAverageTable = new double[image.Bitmap.Width, image.Bitmap.Height];
            outPutImage.countOfAverageTable = new double[image.Bitmap.Width, image.Bitmap.Height];

            for (int i = 0; i < image.Bitmap.Width; i++)
            {
                for (int j = 0; j < image.Bitmap.Height; j++)
                {
                    outPutImage.sumOfAverageTable[i, j] = 0;
                    outPutImage.countOfAverageTable[i, j] = 0;
                }
            }

            DoRandonTransform(image, n, a, l);
            outPutImage.DoReversedRandonTransform();
        }

        public void Display(PictureBox pictureBox)
        {
            pictureBox.Image = Bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DoRandonTransform(BaseImage image, int n, int a, int l)
        {
            CircleCreator cc = new CircleCreator(image.Bitmap.Width - 1, image.Bitmap.Height - 1);
            Circle circle = new Circle(image.Bitmap.Width - 1, image.Bitmap.Height - 1, image.Bitmap.Width / 2, cc.PointsOnCircle.ToArray());
            EmiterGenerator eg = new EmiterGenerator(a, circle);
            List<EmiterDetectorsSystem> systems = new List<EmiterDetectorsSystem>();

            BitmapToBlackAndWhiteConverter blackBitmap = new BitmapToBlackAndWhiteConverter(image.Bitmap);

            averageTable = new double[eg.Emiters.ToArray().Length, n + 1];
            double maxAverage = double.MinValue;

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

                        for(int p = 0; p < lc.Line.Count(); p++)
                        {
                            outPutImage.sumOfAverageTable[lc.Line.ElementAt(p).X, lc.Line.ElementAt(p).Y] += summer.Average;
                            outPutImage.countOfAverageTable[lc.Line.ElementAt(p).X, lc.Line.ElementAt(p).Y] += 1;
                        }
                        
                        averageTable[emiterIndex, detectorIndex++] = summer.Average;

                        if (summer.Average > maxAverage)
                        {
                            maxAverage = summer.Average;
                        }
                    });
                emiterIndex++;
            });

            NormalizeAverageTab(eg.Emiters.ToArray().Length, n + 1, maxAverage);
            GenerateBitmap(eg.Emiters.ToArray().Length, n + 1);

            Console.Write("DoRandonTransform --> DONE\n");
        }

        private void NormalizeAverageTab(int maxX, int maxY, double maxAverage)
        {
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    averageTable[i, j] = averageTable[i, j] * 255 / maxAverage;
                }
            }
        }

        private void GenerateBitmap(int maxX, int maxY)
        {
            Bitmap = new Bitmap(maxX, maxY);

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    int average = Convert.ToInt32(averageTable[i, j]);
                    Color c = Color.FromArgb(average, average, average);
                    Bitmap.SetPixel(i, j, c);
                }
            }
        }
    }
}
