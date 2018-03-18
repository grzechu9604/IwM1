using PierwszyProjekt.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PierwszyProjekt.Images
{
    public class BaseImage : AbstractImage, IBaseImage
    {
        public double[,] SumOfAverageTable;
        public double[,] CountOfAverageTable;
        private const int minimalAmountOfAveragesToBeOnImage = 1;

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
                    if (CountOfAverageTable[i, j] >= minimalAmountOfAveragesToBeOnImage)
                    { 
                        SumOfAverageTable[i, j] = Math.Pow(SumOfAverageTable[i, j], 2) / CountOfAverageTable[i, j];
                        //SumOfAverageTable[i, j] = SumOfAverageTable[i, j]/ CountOfAverageTable[i, j];

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

        public static implicit operator BaseImage(Bitmap v)
        {
            throw new NotImplementedException();
        }
    }
}
