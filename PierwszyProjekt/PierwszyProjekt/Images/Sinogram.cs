﻿using PierwszyProjekt.Algorithms;
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

        public long IterationAmount { set; get; }
        private double Alfa { set; get; }

        public Sinogram(BaseImage image, int n, int a, int l)
        {
            DoRandonTransform(image, n, a, l);
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

            Bitmap = new Bitmap(eg.Emiters.ToArray().Length, n + 1);

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

                        int average = Convert.ToInt32(summer.Average);

                        Color c = Color.FromArgb(average, average, average);
                        Bitmap.SetPixel(emiterIndex, detectorIndex++, c);
                    });
                emiterIndex++;
            });


            Console.Write("DoRandonTransform --> DONE\n");
        }
    }
}
