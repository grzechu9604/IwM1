using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PierwszyProjekt.Images
{
    public class BaseImage : IDisplayable
    {
        public Bitmap Bitmap { private set; get; }
        public double[,] sumOfAverageTable;
        public double[,] countOfAverageTable;

        public BaseImage(string filePath)
        {
            Bitmap = new Bitmap(filePath);
        }

        public BaseImage(int w, int h)
        {
            Bitmap = new Bitmap(w, h);
        }

        public void Display(PictureBox pictureBox)
        {
            pictureBox.Image = Bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void DoReversedRandonTransform()
        {
            double maxValue = 0;

            for (int i = 0; i < Bitmap.Width; i++)
            {
                for (int j = 0; j < Bitmap.Height; j++)
                {
                    sumOfAverageTable[i, j] = sumOfAverageTable[i, j] * countOfAverageTable[i, j] / 255;
                    if (sumOfAverageTable[i, j] > maxValue)
                    {
                        maxValue = sumOfAverageTable[i, j];
                    }
                }
            }

            NormalizeAverageTab(Bitmap.Height, Bitmap.Height, maxValue);

            int rbgValue = 0;
            for (int i = 0; i < Bitmap.Width; i++)
            {
                for (int j = 0; j < Bitmap.Height; j++)
                {
                    rbgValue = Convert.ToInt32(sumOfAverageTable[i, j]);

                    Color c = Color.FromArgb(rbgValue, rbgValue, rbgValue);
                    Bitmap.SetPixel(i, j, c);
                }
            }

            Console.Write("Max is --> " + maxValue + "\n");
            Console.Write("I will do --> DoReversedRandonTransform\n");
        }

        private void NormalizeAverageTab(int maxX, int maxY, double maxAverage)
        {
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    sumOfAverageTable[i, j] = sumOfAverageTable[i, j] * 255 / maxAverage;
                }
            }
        }


        private void setOutPutImage(int width, int height)
        {
            //outPutImage = new BaseImage(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color c = Color.FromArgb(0, 0, 0);
                    //outPutImage.Bitmap.SetPixel(i, j, c);
                }
            }
        }

    }
}
