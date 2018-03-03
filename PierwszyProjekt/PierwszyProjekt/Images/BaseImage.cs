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

        public BaseImage(string filePath)
        {
            Bitmap = new Bitmap(filePath);
        }

        public BaseImage(Sinogram sinogram)
        {
            DoReversedRandonTransform(sinogram);
        }

        public void Display(PictureBox pictureBox)
        {
            pictureBox.Image = Bitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DoReversedRandonTransform(Sinogram sinogram)
        {
            Bitmap = sinogram.Bitmap;
            Console.Write("I will do --> DoReversedRandonTransform\n");
        }
    }
}
