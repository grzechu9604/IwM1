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
    public class BaseImage : AbstractImage
    {
        public double[,] SumOfAverageTable;
        public double[,] CountOfAverageTable;
        private const int minimaleAmountOfAveragToBeOnImage = 0;

        public BaseImage(string filePath)
        {
            Bitmap = new Bitmap(filePath);
        }

        public BaseImage(int w, int h)
        {
            Bitmap = new Bitmap(w, h);
        }

        public void DoReversedRandonTransform()
        {
            double maxValue = double.MinValue;
            double minValue = double.MaxValue;

            for (int i = 0; i < Bitmap.Width; i++)
            {
                for (int j = 0; j < Bitmap.Height; j++)
                {
                    if (CountOfAverageTable[i, j] > minimaleAmountOfAveragToBeOnImage)
                    { 
                        SumOfAverageTable[i, j] = SumOfAverageTable[i, j] / CountOfAverageTable[i, j];

                        if (SumOfAverageTable[i, j] > maxValue)
                        {
                            maxValue = SumOfAverageTable[i, j];
                        }

                        if (SumOfAverageTable[i, j] != 0 && SumOfAverageTable[i, j] < minValue)
                        {
                            minValue = SumOfAverageTable[i, j];
                        }
                    }
                }
            }

            NormalizeTab(Bitmap.Width, Bitmap.Height, maxValue, SumOfAverageTable, minValue);
            GenerateBitmap(Bitmap.Width, Bitmap.Height, SumOfAverageTable);

            Console.Write("Max is --> " + maxValue + "\n");
            Console.Write("DoReversedRandonTransform --> DONE\n");
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
