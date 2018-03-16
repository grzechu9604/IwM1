using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PierwszyProjekt.Algorithms
{
    public class BitmapToBlackAndWhiteConverter
    {
        private const double weightR = 1;
        private const double weightG = 1;
        private const double weightB = 1;

        public double[,] ConvertedTab { private set; get; } 

        public BitmapToBlackAndWhiteConverter(Bitmap bitmap)
        {
            ConvertedTab = new double[bitmap.Width, bitmap.Height];

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color color = bitmap.GetPixel(j, i);
                    ConvertedTab[j, i] = (color.R * weightR + color.G * weightG + color.B * weightB) / (weightB + weightG+ + weightR);
                }
            }
        }
    }
}
