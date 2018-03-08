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
            // TODO: CircleCreator musi tworzyć okrąg, który będzie tworzył taka list punktów, że będą one ze sobą sąsiadować, sprawa istotna przy przeskokach (korzystaniu z symertrii)
            CircleCreator cc = new CircleCreator(image.Bitmap.Width, image.Bitmap.Height);
            Circle circle = new Circle(image.Bitmap.Width, image.Bitmap.Height, image.Bitmap.Width / 2, cc.PointsOnCircle.ToArray());
            EmiterGenerator eg = new EmiterGenerator(a, circle);
            List<EmiterDetectorsSystem> systems = new List<EmiterDetectorsSystem>();

            eg.Emiters.ForEach(e =>
            {
                DetectorsGenerator dg = new DetectorsGenerator(n, l, circle, e);
                systems.Add(new EmiterDetectorsSystem(e, dg.Detectors));
            });
            

            Bitmap = image.Bitmap;
            Color colour;

            /*
            Kolor każdego piksela jest przedstawiana jako 32-bitową liczbą: 8 bity każda alfa, czerwony, zielony i niebieski (ang.).
            Każdy z czterech składników jest liczba z przedziału od 0 do 255, gdzie 0 reprezentuje natężenie do 255 reprezentuje
            pełną intensywność. 
            */
            int[,] bitmapArray = new int[Bitmap.Height, Bitmap.Width];
            for (int i = 0; i < Bitmap.Height; i++)
            {
                for (int j = 0; j < Bitmap.Width; j++)
                {
                    colour = Bitmap.GetPixel(i, j);
                    bitmapArray[i, j] = colour.R + colour.G + colour.B;
                }
            }

            /*
                Czym jest rozpiętość kątowa dla Układu równoległego(np. dla 1 detektora)?
                element_a = 1
                element_n = 1
                element_l = ?
            */


            /*
            int n = 10;
            CircleCreator cc = new CircleCreator(n - 1, n - 1);
            int[,] array = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = 0;
                }
            }

            cc.PointsOnCircle.ForEach(p => array[p.X, p.Y] = 1);
            LineCreator lc = new LineCreator(new Point(0, 6), new Point(0, 8));
            lc.Line.ForEach(p => array[p.X, p.Y] = 5);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
            */

            Console.Write("I will do --> DoRandonTransform\n");
        }
    }
}
