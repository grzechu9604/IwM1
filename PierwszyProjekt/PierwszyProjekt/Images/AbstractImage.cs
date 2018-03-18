using PierwszyProjekt.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PierwszyProjekt.Images
{
    public abstract class AbstractImage
    {
        public Bitmap Bitmap { protected set; get; }

        public void Display(PictureBox pictureBox)
        {
            pictureBox.Image = Bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected void NormalizeTab(int maxX, int maxY, double max, double[,] tabToNormalize, double oldMin = 0, double newMax = 255)
        {
            max = max - oldMin;

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    if (tabToNormalize[i, j] != 0)
                    {
                        tabToNormalize[i, j] = (tabToNormalize[i, j] - oldMin) * newMax / max;
                    }
                }
            }
        }

        protected void GenerateBitmap(int maxX, int maxY, double[,] blacAndWhiteTab)
        {
            Bitmap = new Bitmap(maxX, maxY);

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    int color = Convert.ToInt32(blacAndWhiteTab[i, j]);
                    Color c = Color.FromArgb(color, color, color);
                    Bitmap.SetPixel(i, j, c);
                }
            }
        }
    }
}
