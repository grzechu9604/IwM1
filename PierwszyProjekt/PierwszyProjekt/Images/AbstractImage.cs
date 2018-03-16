using System;
using System.Drawing;
using System.Windows.Forms;

namespace PierwszyProjekt.Images
{
    public class AbstractImage
    {
        public Bitmap Bitmap { protected set; get; }

        public void Display(PictureBox pictureBox)
        {
            pictureBox.Image = Bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected void NormalizeAverageTab(int maxX, int maxY, double maxAverage, double[,] tabTNormalize)
        {
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    tabTNormalize[i, j] = tabTNormalize[i, j] * 255 / maxAverage;
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
                    int average = Convert.ToInt32(blacAndWhiteTab[i, j]);
                    Color c = Color.FromArgb(average, average, average);
                    Bitmap.SetPixel(i, j, c);
                }
            }
        }
    }
}
